using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LTAction : LTNode
{
    public bool done;
    public List<int> resources;
    public string evidence;
    public TimeSpan time = TimeSpan.Zero;
    public LTSubgoal group;

    override public void BtnDoneClicked()
    {
        done = !done;
    }

    public override string GetTimeText()
    {
        string returnText = "";
        returnText += time.Days + " Days";
        return returnText;
    }

    public override string GetResourcesText()
    {
        string returnText = "";
        if (resources.Count == 0) return returnText;
        for(int i = 0; i < LTMainMenu.instance.resources.Count; i++)
        {
            if (resources.Contains(i)) { returnText += LTMainMenu.instance.resources[i] + " | "; }
        }
        return returnText.Remove(returnText.Length - 3);
    }
    public override string GetEvidenceText()
    {
        return evidence;
    }

    public void ToggleResource(int id)
    {
        if (id == 0) return;
        if (resources.Contains(id)) resources.Remove(id);
        else resources.Add(id);
        visualizer.UpdateContense();
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
        Create(newTitle, newPosition, newGroup, false, new List<int>(), "", new TimeSpan());
    }

    public void Create(string newTitle, Vector3 newPosition, LTSubgoal newGroup, bool newDone, List<int> newResources, string newEvidence, TimeSpan newTime)
    {
        Create(newTitle, newPosition);
        group = newGroup;
        done = newDone;
        foreach(var resource in newResources)
        {
            ToggleResource(resource);
        }
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

    public override void EditText(LTEditText.EditType editType, string text)
    {
        switch (editType)
        {
            case LTEditText.EditType.Title:
                title = text;
                break;
            case LTEditText.EditType.Evidence:
                evidence = text;
                break;
            case LTEditText.EditType.Time:
                int x;
                if (int.TryParse(text, out x) && x >= 0)
                {
                    time = new TimeSpan(x, 0, 0, 0);
                }
                break;
            default:
                break;
        }
        visualizer.UpdateContense();
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
        visualizer.DetailsVisible = false;
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
