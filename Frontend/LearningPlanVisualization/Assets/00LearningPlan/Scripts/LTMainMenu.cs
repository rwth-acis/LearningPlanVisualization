﻿using System.Collections;
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
        resources.Add("Toggle Resource");
        //Dummyresources
        resources.Add("Book");
        resources.Add("Teacher");
        resources.Add("Seminar");
        resources.Add("Video");

        ShowClanedar();
        ShowCreateNewTree();
    }


    public void RepositionKeyboard()
    {
        keyboard.RepositionKeyboard(CameraCache.Main.transform.position + CameraCache.Main.transform.forward * 1f - CameraCache.Main.transform.up * 0.2f);
    }

    public void ShowCreateNewTree()
    {
        if (newTreeScreen.transform.localScale == Vector3.zero)
        {
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


        object[] dummyData = { Vector3.zero, subgoalBalls, false, new List<int>(), "Juggle 2 minutes without a flaw", new TimeSpan(2, 0, 0, 0) };
        ((List<int>)dummyData[3]).Add(3);
        ((List<int>)dummyData[3]).Add(1);
        ((List<int>)dummyData[3]).Add(2);

        goal.Create("Juggling");
        subgoalClubs.Create("Clubs");
        subgoalRings.Create("Rings");
        subgoalBalls.Create("Balls");

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
        var goal = FindObjectOfType<LTGoal>();
        var mesh = goal.GetComponentInChildren<MeshFilter>();
        mesh.mesh = newMesh;
    }
    public void RepositionTree()
    {
        var goal = goalSpawner.MostRecentlySpawnedObject.GetComponentInChildren<LTGoal>();
        goal.ResetLevel();
        goal.CalculateLevel(-1);
        goal.RepositionRequirements(repositionMargin);
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
