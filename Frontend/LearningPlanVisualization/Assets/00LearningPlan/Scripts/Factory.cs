﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Factory : MonoBehaviour
{

    i5.Toolkit.Core.Spawners.Spawner goalSpawner;
    i5.Toolkit.Core.Spawners.Spawner subgoalSpawner;
    i5.Toolkit.Core.Spawners.Spawner actionSpawner;
    i5.Toolkit.Core.Spawners.Spawner connectionSpawner;

    public Mesh newMesh;

    public GameObject prefabLTConnection;
    private void Awake()
    {
        var spawner = FindObjectsOfType<i5.Toolkit.Core.Spawners.Spawner>() as i5.Toolkit.Core.Spawners.Spawner[];
        foreach (var go in spawner)
        {
            switch (go.name)
            {
                case "ActionSpawner":
                    actionSpawner = go;
                    break;
                case "SubgoalSpawner":
                    subgoalSpawner = go;
                    break;
                case "GoalSpawner":
                    goalSpawner = go;
                    break;
                case "ConnectionSpawner":
                    connectionSpawner = go;
                    break;
            }
        }
    }
    // Start is called before the first frame update
    void Start()
    {
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Keypad0))
        {

            goalSpawner.Spawn();
            var goal = goalSpawner.MostRecentlySpawnedObject.GetComponentInChildren<LTGoal>();

            subgoalSpawner.Spawn();
            var subgoalClubs = subgoalSpawner.MostRecentlySpawnedObject.GetComponentInChildren<LTSubgoal>();
            subgoalSpawner.Spawn();
            var subgoalRings = subgoalSpawner.MostRecentlySpawnedObject.GetComponentInChildren<LTSubgoal>();
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

            actionSpawner.Spawn();
            var ring5 = actionSpawner.MostRecentlySpawnedObject.GetComponentInChildren<LTAction>();
            actionSpawner.Spawn();
            var ring3 = actionSpawner.MostRecentlySpawnedObject.GetComponentInChildren<LTAction>();
            actionSpawner.Spawn();
            var ring2 = actionSpawner.MostRecentlySpawnedObject.GetComponentInChildren<LTAction>();
            actionSpawner.Spawn();
            var ring1 = actionSpawner.MostRecentlySpawnedObject.GetComponentInChildren<LTAction>();


            actionSpawner.Spawn();
            var clubBurn = actionSpawner.MostRecentlySpawnedObject.GetComponentInChildren<LTAction>();
            actionSpawner.Spawn();
            var club3 = actionSpawner.MostRecentlySpawnedObject.GetComponentInChildren<LTAction>();
            actionSpawner.Spawn();
            var club2 = actionSpawner.MostRecentlySpawnedObject.GetComponentInChildren<LTAction>();
            actionSpawner.Spawn();
            var club1 = actionSpawner.MostRecentlySpawnedObject.GetComponentInChildren<LTAction>();

            goal.Create("Juggling", new Vector3(0, 0, 5));
            subgoalClubs.Create("Clubs", new Vector3(-1.5f, 0, 3.5f));
            subgoalRings.Create("Rings", new Vector3(1.5f, 0, 3.5f));
            subgoalBalls.Create("Balls", new Vector3(0, 0, 2));
            ball5.Create("5 Balls", new Vector3(0, 1, 2), true);
            ball3.Create("3 Balls", new Vector3(1, 2, 2), true);
            ball2.Create("2 Balls", new Vector3(-1, 2, 2), true);
            ball1.Create("1 Ball", new Vector3(0, 3, 2), true);
            ring5.Create("5 Rings", new Vector3(1.5f, 1, 3.5f), true);
            ring3.Create("3 Rings", new Vector3(2.5f, 2, 3.5f), true);
            ring2.Create("2 Rings", new Vector3(0.5f, 2, 3.5f), true);
            ring1.Create("1 Ring", new Vector3(1.5f, 3, 3.5f), true);
            clubBurn.Create("Burning Clubs", new Vector3(-2.5f, 1, 3.5f), false);
            club3.Create("3 Clubs", new Vector3(-0.5f, 1, 3.5f), false);
            club2.Create("2 Clubs", new Vector3(-0.5f, 2, 3.5f), false);
            club1.Create("1 Clubs", new Vector3(-0.5f, 3, 3.5f), true);


            goal.requirements.Add(subgoalClubs);
            goal.requirements.Add(subgoalRings);

            subgoalRings.requirements.Add(subgoalBalls);
            subgoalRings.requirements.Add(ring5);
            subgoalClubs.requirements.Add(subgoalBalls);
            subgoalClubs.requirements.Add(club3);
            subgoalClubs.requirements.Add(clubBurn);
            subgoalBalls.requirements.Add(ball5);

            ball5.requirements.Add(ball3);
            ball5.requirements.Add(ball2);
            ball2.requirements.Add(ball1);
            ball3.requirements.Add(ball1);

            ring5.requirements.Add(ring3);
            ring5.requirements.Add(ring2);
            ring2.requirements.Add(ring1);
            ring3.requirements.Add(ring1);

            club3.requirements.Add(club2);
            club2.requirements.Add(club1);
            clubBurn.requirements.Add(club1);
            

            foreach(var start in goalSpawner.SpawnedInstances)
            {
                var startnode = start.GetComponentInChildren<LTNode>();
                foreach (var endnode in startnode.requirements)
                {
                    connectionSpawner.Spawn();
                    connectionSpawner.MostRecentlySpawnedObject.GetComponent<LTConnection>().Create(startnode, endnode);
                }
            }
            foreach (var start in subgoalSpawner.SpawnedInstances)
            {
                var startnode = start.GetComponentInChildren<LTNode>();
                foreach (var endnode in startnode.requirements)
                {
                    connectionSpawner.Spawn();
                    connectionSpawner.MostRecentlySpawnedObject.GetComponent<LTConnection>().Create(startnode, endnode);
                }
            }
            foreach (var start in actionSpawner.SpawnedInstances)
            {
                var startnode = start.GetComponentInChildren<LTNode>();
                foreach (var endnode in startnode.requirements)
                {
                    connectionSpawner.Spawn();
                    connectionSpawner.MostRecentlySpawnedObject.GetComponent<LTConnection>().Create(startnode, endnode);
                }
            }

        }
        if (Input.GetKeyDown(KeyCode.Keypad1))
        {
            var lTActions = FindObjectsOfType(typeof(LTAction)) as LTAction[];
            foreach (var node in lTActions)
            {
                node.GetComponent<LTNodeVisualizer>().MaterialUpdate();
            }
            var lTSubgoals = FindObjectsOfType(typeof(LTSubgoal)) as LTSubgoal[];
            foreach (var node in lTSubgoals)
            {
                node.UpdateActions();
                node.GetComponent<LTNodeVisualizer>().MaterialUpdate();
            }
            var lTGoals = FindObjectsOfType(typeof(LTGoal)) as LTGoal[];
            foreach (var node in lTGoals)
            {
                node.GetComponent<LTNodeVisualizer>().MaterialUpdate();
            }
        }
        if (Input.GetKeyDown(KeyCode.Keypad2))
        {
            var goal = FindObjectOfType<LTGoal>();
            var mesh = goal.GetComponentInChildren<MeshFilter>();
            mesh.mesh = newMesh;
        }
        if (Input.GetKeyDown(KeyCode.Keypad3))
        {
            var goal = FindObjectOfType<LTGoal>();
            goal.ResetLevel();
            goal.CalculateLevel(-1);
            goal.RepositionRequirements(1.5f);
        }
    }
}
