using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveObject
{
    public class SaveNode
    {
        public int id;
        public Vector3 position;
        public string title;
        public List<int> requirementsId;
    }
    public class SaveGoal : SaveNode { }
    public class SaveSubgoal : SaveNode { }
    public class SaveAction : SaveNode
    {
        public bool done;
        public List<int> resources;
        public string evidence;
        public string time;
        public int groupId;
    }
    public class SavePlannedEvent
    {
        public int actionId;
        public string date;
    }


    public List<SaveGoal> goals;
    public List<SaveSubgoal> subgoals;
    public List<SaveAction> actions;
    public List<SavePlannedEvent> events;
    public List<string> resources;

    public void AddGoal(LTGoal node)
    {
        //TODO
    }
    public void AddSubgoal(LTSubgoal node)
    {
        //TODO
    }
    public void AddAction(LTAction node)
    {
        //TODO
    }
    public void AddEvent(LTCalendar.PlannedEvent plannedEvent)
    {
        this.events.Add(new SavePlannedEvent()
        {
            actionId = plannedEvent.GetAction().id,
            date = plannedEvent.GetStartDate().ToString()
        });
    }
    public void AddResources(List<string> resources)
    {
        this.resources.AddRange(resources);
    }
}
