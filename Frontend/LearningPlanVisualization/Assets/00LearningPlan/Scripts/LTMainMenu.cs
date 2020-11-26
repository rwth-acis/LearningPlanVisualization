using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using I5Spawner = i5.Toolkit.Core.Spawners.Spawner;
using Microsoft.MixedReality.Toolkit.Experimental.UI;

public class LTMainMenu : MonoBehaviour
{
    public static LTMainMenu instance;
    public I5Spawner goalSpawner;
    public I5Spawner subgoalSpawner;
    public I5Spawner actionSpawner;
    public I5Spawner connectionSpawner;
    public NonNativeKeyboard keyboard;
    I5Spawner[] nodeSpawner;
    public Mesh newMesh;

    public delegate void ChangeEditMode(bool editMode);
    public event ChangeEditMode OnChangeEditMode;
    public bool editMode { get; private set; }

    public delegate void CreateConnection(LTNode node);
    public event CreateConnection OnCreateConnection;


    private GameObject connections;

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
    }

    public void CreateDummyTree()
    {
        goalSpawner.Spawn();
        var goal = goalSpawner.MostRecentlySpawnedObject.GetComponentInChildren<LTGoal>();


        subgoalSpawner.Spawn();
        var subgoalBalls = subgoalSpawner.MostRecentlySpawnedObject.GetComponentInChildren<LTSubgoal>();

        actionSpawner.Spawn();
        var ball5 = actionSpawner.MostRecentlySpawnedObject.GetComponentInChildren<LTAction>();
        actionSpawner.MostRecentlySpawnedObject.transform.SetParent(subgoalSpawner.MostRecentlySpawnedObject.transform);
        actionSpawner.Spawn();
        var ball3 = actionSpawner.MostRecentlySpawnedObject.GetComponentInChildren<LTAction>();
        actionSpawner.MostRecentlySpawnedObject.transform.SetParent(subgoalSpawner.MostRecentlySpawnedObject.transform);
        actionSpawner.Spawn();
        var ball2 = actionSpawner.MostRecentlySpawnedObject.GetComponentInChildren<LTAction>();
        actionSpawner.MostRecentlySpawnedObject.transform.SetParent(subgoalSpawner.MostRecentlySpawnedObject.transform);
        actionSpawner.Spawn();
        var ball1 = actionSpawner.MostRecentlySpawnedObject.GetComponentInChildren<LTAction>();
        actionSpawner.MostRecentlySpawnedObject.transform.SetParent(subgoalSpawner.MostRecentlySpawnedObject.transform);


        subgoalSpawner.Spawn();
        var subgoalRings = subgoalSpawner.MostRecentlySpawnedObject.GetComponentInChildren<LTSubgoal>();

        actionSpawner.Spawn();
        var ring5 = actionSpawner.MostRecentlySpawnedObject.GetComponentInChildren<LTAction>();
        actionSpawner.MostRecentlySpawnedObject.transform.SetParent(subgoalSpawner.MostRecentlySpawnedObject.transform);
        actionSpawner.Spawn();
        var ring3 = actionSpawner.MostRecentlySpawnedObject.GetComponentInChildren<LTAction>();
        actionSpawner.MostRecentlySpawnedObject.transform.SetParent(subgoalSpawner.MostRecentlySpawnedObject.transform);
        actionSpawner.Spawn();
        var ring2 = actionSpawner.MostRecentlySpawnedObject.GetComponentInChildren<LTAction>();
        actionSpawner.MostRecentlySpawnedObject.transform.SetParent(subgoalSpawner.MostRecentlySpawnedObject.transform);
        actionSpawner.Spawn();
        var ring1 = actionSpawner.MostRecentlySpawnedObject.GetComponentInChildren<LTAction>();
        actionSpawner.MostRecentlySpawnedObject.transform.SetParent(subgoalSpawner.MostRecentlySpawnedObject.transform);

        
        subgoalSpawner.Spawn();
        var subgoalClubs = subgoalSpawner.MostRecentlySpawnedObject.GetComponentInChildren<LTSubgoal>();

        actionSpawner.Spawn();
        var clubBurn = actionSpawner.MostRecentlySpawnedObject.GetComponentInChildren<LTAction>();
        actionSpawner.MostRecentlySpawnedObject.transform.SetParent(subgoalSpawner.MostRecentlySpawnedObject.transform);
        actionSpawner.Spawn();
        var club3 = actionSpawner.MostRecentlySpawnedObject.GetComponentInChildren<LTAction>();
        actionSpawner.MostRecentlySpawnedObject.transform.SetParent(subgoalSpawner.MostRecentlySpawnedObject.transform);
        actionSpawner.Spawn();
        var club2 = actionSpawner.MostRecentlySpawnedObject.GetComponentInChildren<LTAction>();
        actionSpawner.MostRecentlySpawnedObject.transform.SetParent(subgoalSpawner.MostRecentlySpawnedObject.transform);
        actionSpawner.Spawn();
        var club1 = actionSpawner.MostRecentlySpawnedObject.GetComponentInChildren<LTAction>();
        actionSpawner.MostRecentlySpawnedObject.transform.SetParent(subgoalSpawner.MostRecentlySpawnedObject.transform);


        var dummyResources = new List<string>();
        dummyResources.Add("Test Resource");
        dummyResources.Add("Second One");
        dummyResources.Add("And a Third");
        dummyResources.Add("Last One");

        goal.Create("Juggling");
        subgoalClubs.Create("Clubs");
        subgoalRings.Create("Rings");
        subgoalBalls.Create("Balls");
        ball5.Create("5 Balls", Vector3.zero, subgoalBalls, true, dummyResources, "Juggle 2 minutes without a flaw", new TimeSpan(2, 0, 0, 0));
        ball3.Create("3 Balls", Vector3.zero, subgoalBalls, true, dummyResources, "Juggle 2 minutes without a flaw", new TimeSpan(2, 0, 0, 0));
        ball2.Create("2 Balls", Vector3.zero, subgoalBalls, true, dummyResources, "Juggle 2 minutes without a flaw", new TimeSpan(2, 0, 0, 0));
        ball1.Create("1 Ball", Vector3.zero, subgoalBalls, true, dummyResources, "Juggle 2 minutes without a flaw", new TimeSpan(2, 0, 0, 0));
        ring5.Create("5 Rings", Vector3.zero, subgoalRings, true, dummyResources, "Juggle 2 minutes without a flaw", new TimeSpan(2, 0, 0, 0));
        ring3.Create("3 Rings", Vector3.zero, subgoalRings, true, dummyResources, "Juggle 2 minutes without a flaw", new TimeSpan(2, 0, 0, 0));
        ring2.Create("2 Rings", Vector3.zero, subgoalRings, true, dummyResources, "Juggle 2 minutes without a flaw", new TimeSpan(2, 0, 0, 0));
        ring1.Create("1 Ring", Vector3.zero, subgoalRings, true, dummyResources, "Juggle 2 minutes without a flaw", new TimeSpan(2, 0, 0, 0));
        clubBurn.Create("Burning Clubs", Vector3.zero, subgoalClubs, false, dummyResources, "Juggle 2 minutes without a flaw", new TimeSpan(2, 0, 0, 0));
        club3.Create("3 Clubs", Vector3.zero, subgoalClubs, false, dummyResources, "Juggle 2 minutes without a flaw", new TimeSpan(2, 0, 0, 0));
        club2.Create("2 Clubs", Vector3.zero, subgoalClubs, false, dummyResources, "Juggle 2 minutes without a flaw", new TimeSpan(2, 0, 0, 0));
        club1.Create("1 Clubs", Vector3.zero, subgoalClubs, true, dummyResources, "Juggle 2 minutes without a flaw", new TimeSpan(2, 0, 0, 0));

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
        }
        RepositionTree();
    }

    public void ChangeGoalMesh()
    {
        var goal = FindObjectOfType<LTGoal>();
        var mesh = goal.GetComponentInChildren<MeshFilter>();
        mesh.mesh = newMesh;
    }

    public void RepositionTree()
    {
        var goal = goalSpawner.MostRecentlySpawnedObject.GetComponentInChildren<LTGoal>();
        goal.ResetLevel();
        goal.CalculateLevel(-1);
        goal.RepositionRequirements(0.2f);
    }

    public void SwitchEditMode()
    {
        editMode = !editMode;
        OnChangeEditMode?.Invoke(editMode);
    }

    public void InvokeCreateConnection(LTNode node)
    {
        OnCreateConnection?.Invoke(node);
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

}
