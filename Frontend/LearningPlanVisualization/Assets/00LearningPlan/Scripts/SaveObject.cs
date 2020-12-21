using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class SaveObject
{
    [Serializable]
    public struct SaveGoal
    {
        public int id;
        public Vector3 position;
        public string title;
    }
    [Serializable]
    public struct SaveSubgoal
    {
        public int id;
        public Vector3 position;
        public string title;
        public int groupId;
    }
    [Serializable]
    public struct SaveAction
    {
        public int id;
        public Vector3 position;
        public string title;
        public bool done;
        public List<int> resources;
        public string evidence;
        public string time;
        public int groupId;
    }
    [Serializable]
    public struct SavePlannedEvent
    {
        public int actionId;
        public string date;
    }

    [Serializable]
    public struct SaveConnection
    {
        public int startId;
        public int endId;
    }

    
    public List<SaveGoal> goals = new List<SaveGoal>();
    public List<SaveSubgoal> subgoals = new List<SaveSubgoal>();
    public List<SaveAction> actions = new List<SaveAction>();
    public List<SavePlannedEvent> events = new List<SavePlannedEvent>();
    public List<SaveConnection> connections = new List<SaveConnection>();
    public List<string> resources = new List<string>();


    public void AddGoal(LTGoal node)
    {
        var goal = new SaveGoal()
        {
            id = node.id,
            position = node.transform.position,
            title = node.title
        };
        AddConnection(node);
        goals.Add(goal);
    }

    public void AddSubgoal(LTSubgoal node)
    {
        var subgoal = new SaveSubgoal()
        {
            id = node.id,
            position = node.transform.position,
            title = node.title,
            groupId = node.group.id
        };
        AddConnection(node);
        subgoals.Add(subgoal);
    }

    public void AddAction(LTAction node)
    {
        var action = new SaveAction()
        {
            id = node.id,
            position = node.transform.position,
            title = node.title,
            done=node.done,
            resources=node.resources,
            evidence=node.evidence,
            time=node.time.ToString(),
            groupId=node.group.id
        };
        AddConnection(node);
        actions.Add(action);
    }

    public void AddEvent(LTCalendar.PlannedEvent plannedEvent)
    {
        events.Add(new SavePlannedEvent()
        {
            actionId = plannedEvent.GetAction().id,
            date = plannedEvent.GetStartDate().ToString()
        });
    }
    public void AddConnection(LTNode startNode)
    {
        foreach (var req in startNode.requirements)
        {
            connections.Add(new SaveConnection()
            {
                startId = startNode.id,
                endId = req.id
            });
        }
    }
    public void AddResources(List<string> resources)
    {
        this.resources.AddRange(resources);
    }
}
