using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LTAction : LTNode
{
    public bool done;
    public List<string> resources;
    public string evidence;
    public TimeSpan time = TimeSpan.Zero;
    public LTSubgoal group;
    override public void BtnDoneClicked()
    {
        done = !done;
    }

    override public string GetDetailsText()
    {
        string returnText = "";
        returnText += "<u><size=140%><align=\"center\">"+title+"</align></size></u>\n\n";

        returnText += "Resources:\n";
        returnText += "<indent=2em>";
        foreach(var resource in resources){ returnText += resource + " | "; }
        returnText += "</indent>\n";

        returnText += "Evidence:\n";
        returnText += "<indent=2em>" + evidence + "</indent>\n";

        returnText += "Time:\n";
        returnText += "<indent=2em>" + time.Days + " Days | " + time.Hours + " Hours | " + time.Minutes + " Minutes</indent>";
        return returnText;
    }

    override public void RepositionRequirements(float margin)
    {
        var offset = requirements.Count - 1f;
        var i = 0;
        foreach (var requirement in requirements)
        {
            if (level + 1 == requirement.level)
            {
                requirement.transform.position = transform.position + new Vector3((-offset + 2 * i) * margin, margin, 0);
                requirement.RepositionRequirements(margin);
            }
            i++;
        }
    }
    public override void NodeClicked()
    {
        visualizer.DetailsVisible = !visualizer.DetailsVisible;
    }
    public void Create(string newTitle, Vector3 newPosition, LTSubgoal newGroup)
    {
        Create(newTitle, newPosition, newGroup, false, new List<string>(), "", new TimeSpan());
    }

    public void Create(string newTitle, Vector3 newPosition, LTSubgoal newGroup, bool newDone, List<string> newResources, string newEvidence, TimeSpan newTime)
    {
        Create(newTitle, newPosition);
        group = newGroup;
        done = newDone;
        resources = newResources;
        evidence = newEvidence;
        time = newTime;

    }


    override public void UpdateStatus()
    {
        if (done) status = LTStatus.Done;
        else if (group.status == LTStatus.NotAvailable) status = LTStatus.NotAvailable;
        else
        {
            foreach (var node in requirements)
            {
                if (!(node.status == LTStatus.Done))
                {
                    status = LTStatus.NotAvailable;
                    return;
                }
            }
            status = LTStatus.Available;
        }
    }

    override public void UpdateCalendarStatus()
    {
        if (calendarStatus == LTStatus.Done) return;
        if (group.calendarStatus == LTStatus.NotAvailable) calendarStatus = LTStatus.NotAvailable;
        else
        {
            foreach (var node in requirements)
            {
                if (!(node.calendarStatus == LTStatus.Done))
                {
                    calendarStatus = LTStatus.NotAvailable;
                    return;
                }
            }
            calendarStatus = LTStatus.Available;
        }
    }

    public override void HandleChangeMode(LTModes oldMode, LTModes newMode)
    {
        switch (oldMode)
        {
            case LTModes.Normal:
                break;
            case LTModes.CreateConnection:
                break;
            case LTModes.CreateGhost:
                break;
            case LTModes.AddToCalendar:
                EndAddToCalendar();
                break;
            default:
                break;
        }
        switch (newMode)
        {
            case LTModes.Normal:
                break;
            case LTModes.CreateConnection:
                break;
            case LTModes.CreateGhost:
                break;
            case LTModes.AddToCalendar:
                BeginAddToCalendar();
                break;
            default:
                break;
        }
    }

    private void BeginAddToCalendar()
    {
        if (calendarStatus == LTStatus.Available)
        {
            group.SetExpanded(true);
            addToCalendarSphere.SetActive(true);
        }
    }

    private void EndAddToCalendar()
    {
        addToCalendarSphere.SetActive(false);
    }
}
