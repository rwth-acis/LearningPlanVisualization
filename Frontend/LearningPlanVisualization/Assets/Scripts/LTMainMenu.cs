using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using I5Spawner = i5.Toolkit.Core.Spawners.Spawner;
using Microsoft.MixedReality.Toolkit.Experimental.UI;
using Microsoft.MixedReality.Toolkit.Utilities;

public enum LTModes { Normal, CreateConnection, CreateGhost, AddToCalendar }
public class LTMainMenu : MonoBehaviour
{
    public static LTMainMenu instance;
    public I5Spawner goalSpawner;
    public I5Spawner subgoalSpawner;
    public I5Spawner actionSpawner;
    public I5Spawner connectionSpawner;
    public NonNativeKeyboard keyboard;
    public I5Spawner[] nodeSpawner;
    public LTCalendar calendar;
    public InfoScreen newTreeScreen;
    public Mesh newMesh;
    public float repositionMargin = 0.2f;

    public int nextId=0;
    public List<string> resources = new List<string>();

    public delegate void ChangeEditMode(bool editMode);
    public event ChangeEditMode OnChangeEditMode;
    public bool editMode { get; private set; }

    public delegate void ChangeMode(LTModes newMode, LTModes oldMode);
    public event ChangeMode OnChangeMode;
    public LTModes mode { get; private set; }



    public delegate void CreateConnection(LTNode node);
    public event CreateConnection OnCreateConnection;

    public delegate void DestroyGhosts();
    public event DestroyGhosts OnDestroyGhosts;




    private GameObject connections;

    private Vector3 calendarScale = new Vector3(0.5f, 0.5f, 1);
    private Vector3 newTreeScreenScale = new Vector3(0.3f, 0.3f, 0.3f);

    private void Awake()
    {
        instance = this;
        nodeSpawner = new I5Spawner[] { goalSpawner, subgoalSpawner, actionSpawner };
        editMode = false;
    }

    private void Start()
    {
        OnChangeEditMode?.Invoke(editMode);
        connections = new GameObject("Connections");
        ResetResources();
        SaveSystem.Init();

        //print(SaveSystem.SAVE_FOLDER);

        ShowClanedar();
        ShowCreateNewTree();
    }
    private void ResetResources()
    {

        resources.Clear();
        resources.Add("Toggle Resource");
        resources.Add("New Resource...");
        //Dummyresources
        resources.Add("Book");
        resources.Add("Teacher");
        resources.Add("Seminar");
        resources.Add("Video");
    }

    public void SaveTree(string fileName)
    {
        SaveObject saveObject = new SaveObject() { resources = resources };
        foreach (var node in goalSpawner.SpawnedInstances)
            saveObject.AddGoal(node.GetComponentInChildren<LTGoal>());
        foreach (var node in subgoalSpawner.SpawnedInstances)
            saveObject.AddSubgoal(node.GetComponentInChildren<LTSubgoal>());
        foreach (var node in actionSpawner.SpawnedInstances)
            saveObject.AddAction(node.GetComponentInChildren<LTAction>());
        foreach (var plannedEvent in calendar.plannedEvents)
            saveObject.AddEvent(plannedEvent);
        
        string saveString = JsonUtility.ToJson(saveObject);
        SaveSystem.Save(fileName, saveString);
    }
    public void SaveButtonClicked()
    {
        SaveTree("save.txt");
    }

    public void LoadTree(string fileName)
    {
        if (goalSpawner.SpawnedInstances.Length!=0)
        {
            DeleteTree();
            return;
        }
        string saveString = SaveSystem.Load(fileName);
        if (saveString == null) print("File not Found");
        else
        {

            SaveObject saveObject = JsonUtility.FromJson<SaveObject>(saveString);
            var highestID = 0;
            var offsetID = nextId;
            resources = saveObject.resources;
            //Create Nodes
            foreach(var savedNode in saveObject.goals)
            {
                if (savedNode.id > highestID) highestID = savedNode.id;
                goalSpawner.Spawn();
                var node = goalSpawner.MostRecentlySpawnedObject.GetComponentInChildren<LTGoal>();
                node.Create(savedNode.title,savedNode.position);
                node.id = offsetID+savedNode.id;
            }
            foreach (var savedNode in saveObject.subgoals)
            {
                if (savedNode.id > highestID) highestID = savedNode.id;
                subgoalSpawner.Spawn();
                var node = subgoalSpawner.MostRecentlySpawnedObject.GetComponentInChildren<LTSubgoal>();
                node.Create(savedNode.title, savedNode.position,GetNodeByID(offsetID+savedNode.groupId) as LTGoal);
                node.id = offsetID+savedNode.id;
            }
            foreach (var savedNode in saveObject.actions)
            {
                if (savedNode.id > highestID) highestID = savedNode.id;
                actionSpawner.Spawn();
                var node = actionSpawner.MostRecentlySpawnedObject.GetComponentInChildren<LTAction>();
                node.Create(savedNode.title, savedNode.position, GetNodeByID(offsetID+savedNode.groupId) as LTSubgoal, savedNode.done, savedNode.resources, savedNode.evidence, TimeSpan.Parse(savedNode.time));
                node.id = offsetID+savedNode.id;
            }
            nextId = offsetID+highestID + 1;
            //Create Connections
            foreach(var savedConnection in saveObject.connections)
            {
                NewConnection(offsetID+savedConnection.startId, offsetID+savedConnection.endId);
            }
            //Create Events
            foreach(var savedEvent in saveObject.events)
            {
                calendar.AddEvent(GetNodeByID(offsetID+savedEvent.actionId) as LTAction, DateTime.Parse(savedEvent.date));
            }
            //Change Goalmesh
            ChangeGoalMesh();
        }
    }

    public void LoadButtonClicked()
    {
        LoadTree("save.txt");
    }

    public void DeleteTree()
    {
        calendar.ClearEvents();

        foreach(var goal in goalSpawner.SpawnedInstances)
        {
            goal.GetComponentInChildren<LTGoal>().Delete();
        }
        ResetResources();
        nextId = 0;
    }
    
    public void RepositionKeyboard()
    {

        keyboard.Close();
        keyboard.RepositionKeyboard(CameraCache.Main.transform.position + CameraCache.Main.transform.forward * 1.5f - CameraCache.Main.transform.up * 0.2f);
    }

    public void ShowCreateNewTree()
    {
        if (newTreeScreen.transform.localScale == Vector3.zero)
        {
            if (newTreeScreen.Step == InfoScreenStep.DefineGoal)
                DeleteTree();
            newTreeScreen.transform.localScale = newTreeScreenScale;
            newTreeScreen.Step = InfoScreenStep.DefineGoal;
        }
        else
        {
            newTreeScreenScale = newTreeScreen.transform.localScale;
            newTreeScreen.transform.localScale = Vector3.zero;
        }
    }
    public void CreateDummyTree()
    {
        goalSpawner.Spawn();
        var goal = goalSpawner.MostRecentlySpawnedObject.GetComponentInChildren<LTGoal>();


        subgoalSpawner.Spawn();
        var subgoalBalls = subgoalSpawner.MostRecentlySpawnedObject.GetComponentInChildren<LTSubgoal>();

        actionSpawner.Spawn();
        var ball5 = actionSpawner.MostRecentlySpawnedObject.GetComponentInChildren<LTAction>();
        actionSpawner.Spawn();
        var ball3 = actionSpawner.MostRecentlySpawnedObject.GetComponentInChildren<LTAction>();
        actionSpawner.Spawn();
        var ball2 = actionSpawner.MostRecentlySpawnedObject.GetComponentInChildren<LTAction>();
        actionSpawner.Spawn();
        var ball1 = actionSpawner.MostRecentlySpawnedObject.GetComponentInChildren<LTAction>();
        

        subgoalSpawner.Spawn();
        var subgoalRings = subgoalSpawner.MostRecentlySpawnedObject.GetComponentInChildren<LTSubgoal>();

        actionSpawner.Spawn();
        var ring5 = actionSpawner.MostRecentlySpawnedObject.GetComponentInChildren<LTAction>();
        actionSpawner.Spawn();
        var ring3 = actionSpawner.MostRecentlySpawnedObject.GetComponentInChildren<LTAction>();
        actionSpawner.Spawn();
        var ring2 = actionSpawner.MostRecentlySpawnedObject.GetComponentInChildren<LTAction>();
        actionSpawner.Spawn();
        var ring1 = actionSpawner.MostRecentlySpawnedObject.GetComponentInChildren<LTAction>();
        

        subgoalSpawner.Spawn();
        var subgoalClubs = subgoalSpawner.MostRecentlySpawnedObject.GetComponentInChildren<LTSubgoal>();

        actionSpawner.Spawn();
        var clubBurn = actionSpawner.MostRecentlySpawnedObject.GetComponentInChildren<LTAction>();
        actionSpawner.Spawn();
        var club3 = actionSpawner.MostRecentlySpawnedObject.GetComponentInChildren<LTAction>();
        actionSpawner.Spawn();
        var club2 = actionSpawner.MostRecentlySpawnedObject.GetComponentInChildren<LTAction>();
        actionSpawner.Spawn();
        var club1 = actionSpawner.MostRecentlySpawnedObject.GetComponentInChildren<LTAction>();
        

        object[] dummyData = { Vector3.zero, subgoalBalls, false, new List<int>(), "Juggle 2 minutes without a flaw", new TimeSpan(2, 0, 0, 0) };
        ((List<int>)dummyData[3]).Add(3);
        ((List<int>)dummyData[3]).Add(4);
        ((List<int>)dummyData[3]).Add(2);

        goal.Create("Juggling");
        subgoalClubs.Create("Clubs", Vector3.zero, goal);
        subgoalRings.Create("Rings", Vector3.zero, goal);
        subgoalBalls.Create("Balls", Vector3.zero, goal);

        ball5.Create("5 Balls", (Vector3)dummyData[0],subgoalBalls, (bool)dummyData[2],(List<int>)dummyData[3],(String)dummyData[4],(TimeSpan)dummyData[5]);
        ball3.Create("3 Balls", (Vector3)dummyData[0], subgoalBalls, (bool)dummyData[2], (List<int>)dummyData[3], (String)dummyData[4], (TimeSpan)dummyData[5]);
        ball2.Create("2 Balls", (Vector3)dummyData[0], subgoalBalls, (bool)dummyData[2], (List<int>)dummyData[3], (String)dummyData[4], (TimeSpan)dummyData[5]);
        ball1.Create("1 Ball", (Vector3)dummyData[0], subgoalBalls, (bool)dummyData[2], (List<int>)dummyData[3], (String)dummyData[4], (TimeSpan)dummyData[5]);
        ring5.Create("5 Rings", (Vector3)dummyData[0], subgoalRings, (bool)dummyData[2], (List<int>)dummyData[3], (String)dummyData[4], (TimeSpan)dummyData[5]);
        ring3.Create("3 Rings", (Vector3)dummyData[0], subgoalRings, (bool)dummyData[2], (List<int>)dummyData[3], (String)dummyData[4], (TimeSpan)dummyData[5]);
        ring2.Create("2 Rings", (Vector3)dummyData[0], subgoalRings, (bool)dummyData[2], (List<int>)dummyData[3], (String)dummyData[4], (TimeSpan)dummyData[5]);
        ring1.Create("1 Ring", (Vector3)dummyData[0], subgoalRings, (bool)dummyData[2], (List<int>)dummyData[3], (String)dummyData[4], (TimeSpan)dummyData[5]);
        clubBurn.Create("Burning Clubs", (Vector3)dummyData[0], subgoalClubs, (bool)dummyData[2], (List<int>)dummyData[3], (String)dummyData[4], (TimeSpan)dummyData[5]);
        club3.Create("3 Clubs", (Vector3)dummyData[0], subgoalClubs, (bool)dummyData[2], (List<int>)dummyData[3], (String)dummyData[4], (TimeSpan)dummyData[5]);
        club2.Create("2 Clubs", (Vector3)dummyData[0], subgoalClubs, (bool)dummyData[2], (List<int>)dummyData[3], (String)dummyData[4], (TimeSpan)dummyData[5]);
        club1.Create("1 Club", (Vector3)dummyData[0], subgoalClubs, (bool)dummyData[2], (List<int>)dummyData[3], (String)dummyData[4], (TimeSpan)dummyData[5]);

        NewConnection(goal, subgoalClubs);
        NewConnection(goal, subgoalRings);
        
        NewConnection(subgoalRings, subgoalBalls);
        NewConnection(subgoalRings, ring5);
        NewConnection(subgoalClubs, subgoalBalls);
        NewConnection(subgoalClubs, club3);
        NewConnection(subgoalClubs, clubBurn);
        NewConnection(subgoalBalls, ball5);

        NewConnection(ball5, ball3);
        NewConnection(ball5, ball2);
        NewConnection(ball2, ball1);
        NewConnection(ball3, ball1);

        NewConnection(ring5, ring3);
        NewConnection(ring5, ring2);
        NewConnection(ring3, ring1);
        NewConnection(ring2, ring1);

        NewConnection(club3,club2);
        NewConnection(club2,club1);
        NewConnection(clubBurn,club1);

        foreach (var start in subgoalSpawner.SpawnedInstances)
        {
            start.GetComponentInChildren<LTSubgoal>().UpdateActions();
            start.GetComponentInChildren<LTSubgoal>().SetExpanded(false);
        }
        RepositionTree();
        ChangeGoalMesh();
    }
    public void ChangeGoalMesh()
    {
        foreach( var goal in goalSpawner.SpawnedInstances)
            goal.GetComponentInChildren<MeshFilter>().mesh=newMesh;
    }
    public void RepositionTree()
    {
        foreach(var node in goalSpawner.SpawnedInstances)
        {
            var goal = node.GetComponentInChildren<LTGoal>();
            goal.ResetLevel();
            goal.CalculateLevel(-1);
            goal.RepositionRequirements(repositionMargin);
        }
    }
    public void ShowClanedar()
    {
        if (calendar.transform.localScale == Vector3.zero)
        {
            calendar.transform.localScale = calendarScale;
        }
        else
        {
            calendarScale = calendar.transform.localScale;
            calendar.transform.localScale = Vector3.zero;
        }
    }
    public void SwitchEditMode()
    {
        SwitchEditMode(!editMode);
    }
    public void SwitchEditMode(bool value)
    {
        editMode = value;
        OnChangeEditMode?.Invoke(editMode);
    }

    public void SwitchMode(LTModes newMode)
    {
        OnChangeMode?.Invoke(mode, newMode);
        mode = newMode;
    }

    public void InvokeCreateConnection(LTNode node)
    {
        OnCreateConnection?.Invoke(node);
    }

    public void InvokeDestroyGhosts()
    {
        OnDestroyGhosts?.Invoke();
    }
    
    public void NewConnection(LTNode startnode, LTNode endnode)
    {
        if(startnode && endnode)
        {
            connectionSpawner.Spawn();
            connectionSpawner.MostRecentlySpawnedObject.GetComponent<LTConnection>().Create(startnode, endnode);
            connectionSpawner.MostRecentlySpawnedObject.transform.SetParent(connections.transform);
            startnode.requirements.Add(endnode);
            startnode.GetComponentInParent<LTNodeVisualizer>().MaterialUpdate();
        }
    }

    public void NewConnection(int startNodeID, int endNodeID)
    {
        NewConnection(GetNodeByID(startNodeID), GetNodeByID(endNodeID));
    }

    public bool AreConnected(LTNode firstNode, LTNode secondNode)
    {
        if (AreConnectedInOrder(firstNode, secondNode)) return true;
        if (AreConnectedInOrder(secondNode, firstNode)) return true;
        return false;
    }

    private bool AreConnectedInOrder(LTNode startnode, LTNode endnode)
    {
        foreach (var requirement in startnode.requirements)
        {
            if (requirement == endnode) return true;
        }
        return false;
    }

    public LTNode GetNodeByID(int id)
    {
        LTNode node;
        node = Array.Find(goalSpawner.SpawnedInstances, x => x.GetComponentInChildren<LTGoal>().id == id)?.GetComponentInChildren<LTGoal>();
        if (node != default(LTNode)) return node;
        node = Array.Find(subgoalSpawner.SpawnedInstances, x => x.GetComponentInChildren<LTSubgoal>().id == id)?.GetComponentInChildren<LTSubgoal>();
        if (node != default(LTNode)) return node;
        node = Array.Find(actionSpawner.SpawnedInstances, x => x.GetComponentInChildren<LTAction>().id == id)?.GetComponentInChildren<LTAction>();
        if (node != default(LTNode)) return node;
        return null;
    }
    IEnumerator WaitFrame()
    {
        yield return null;
    }
}
