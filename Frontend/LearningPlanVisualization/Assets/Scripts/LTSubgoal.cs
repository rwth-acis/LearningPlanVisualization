﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LTSubgoal : LTNode
{
    public int neededActions { get; private set; }
    public int doneActions { get; private set; }
    public LTGoal group;

    public override void NodeClicked()
    {
        base.NodeClicked();
        SetExpanded(!visibility.RequirementsVisible);
    }

    public void Create(string newTitle,Vector3 newPosition, LTGoal newGroup)
    {
        Create(newTitle, newPosition);
        group = newGroup;
    }

    override public void RepositionRequirements(float margin)
    {
        List<LTAction> requiredActions = new List<LTAction>();
        List<LTSubgoal> requiredSubgoals = new List<LTSubgoal>();
        foreach (var requirement in requirements)
        {
            if(level + 1 == requirement.level)
            {
                if (requirement.GetType() == typeof(LTAction)) requiredActions.Add(requirement as LTAction);
                if (requirement.GetType() == typeof(LTSubgoal)) requiredSubgoals.Add(requirement as LTSubgoal);
            }
        }

        var offset = requiredSubgoals.Count - 1f;
        var i = 0;
        foreach (var requirement in requiredSubgoals)
        {
            requirement.transform.position = transform.position + new Vector3((-offset + 2 * i) * margin, 0, -margin);
            requirement.RepositionRequirements(margin);
            i++;
        }


        offset = requiredActions.Count - 1f;
        i = 0;
        foreach (var requirement in requiredActions)
        {
            requirement.transform.position = transform.position + new Vector3((-offset + 2 * i) * margin*0.7f, margin, 0);
            requirement.RepositionRequirements(margin * 0.7f);
            i++;
        }
    }

    public override string GetTitleText()
    {
        var returnString = title;
        returnString += "\n<align=\"right\"><size=60%>"+doneActions+"/"+neededActions+"</size></align>";
        return returnString;
    }

    public void UpdateActions()
    {
        var actions = GetAllRequiredActions();
        neededActions = 0;
        doneActions = 0;
        foreach(var action in actions)
        {
            neededActions++;
            if (action.done) doneActions++;
        }
    }

    public List<LTAction> GetAllRequiredActions()
    {
        List<LTAction> returnList = new List<LTAction>();

        foreach (var item in LTMainMenu.instance.actionSpawner.SpawnedInstances)
        {
            var action = item.GetComponentInChildren<LTAction>();
            if (action.group == this) returnList.Add(action);
        }
        return returnList;
    }

    override public void UpdateStatus()
    {
        var notDone = false;
        foreach (var node in requirements)
        {
            if (!(node.status == LTStatus.Done))
            {
                if(node.GetType() == typeof(LTSubgoal))
                {
                    status = LTStatus.NotAvailable;
                    return;
                }else if(node.GetType() == typeof(LTAction))
                {
                    notDone = true;
                }
            }
        }
        if (notDone) status = LTStatus.Available;
        else status = LTStatus.Done;
    }

    override public void UpdateCalendarStatus()
    {
        var notDone = false;
        foreach (var node in requirements)
        {
            if (!(node.calendarStatus == LTStatus.Done))
            {
                if (node.GetType() == typeof(LTSubgoal))
                {
                    calendarStatus = LTStatus.NotAvailable;
                    return;
                }
                else if (node.GetType() == typeof(LTAction))
                {
                    notDone = true;
                }
            }
        }
        if (notDone) calendarStatus = LTStatus.Available;
        else calendarStatus = LTStatus.Done;
    }

    public override void Delete()
    {
        var actions = GetAllRequiredActions();
        foreach (var action in actions)
        {
            action.Delete();
        }
        base.Delete();
    }
    public void SetExpanded(bool value)
    {
        visibility.RequirementsVisible = value;
    }
}
