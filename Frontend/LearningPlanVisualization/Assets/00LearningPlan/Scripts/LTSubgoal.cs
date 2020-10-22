using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LTSubgoal : LTNode
{
    public int neededActions { get; private set; }
    public int doneActions { get; private set; }
    public List<LTAction> actions { get; private set; }

    public override void NodeClicked()
    {

        base.NodeClicked();
        visibility.RequirementsVisible = !visibility.RequirementsVisible;
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
        actions = GetAllRequirements();
        neededActions = 0;
        doneActions = 0;
        foreach(var action in actions)
        {
            neededActions++;
            if (action.done) doneActions++;
        }
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

    private void Start()
    {
        visualizer = GetComponent<LTNodeVisualizer>();
        visibility = GetComponent<Visibility>();
    }
}
