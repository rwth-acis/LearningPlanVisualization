using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LTGoal : LTNode
{
    override public void RepositionRequirements(float margin)
    {
        var offset = requirements.Count-1f;
        var i = 0;
        foreach (var requirement in requirements)
        {
            requirement.transform.position = transform.position + new Vector3((-offset+2*i)*margin,0,-margin);
            requirement.RepositionRequirements(margin);
            i++;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        visualizer = GetComponent<LTNodeVisualizer>();
        visibility = GetComponent<Visibility>();
    }
    
    override public void UpdateStatus()
    {
        foreach (var node in requirements)
        {
            if (!(node.status == LTStatus.Done))
            {
                status = LTStatus.NotAvailable;
                return;
            }
        }
        status = LTStatus.Done;
    }

    override public void UpdateCalendarStatus()
    {
        foreach (var node in requirements)
        {
            if (!(node.calendarStatus == LTStatus.Done))
            {
                calendarStatus = LTStatus.NotAvailable;
                return;
            }
        }
        calendarStatus = LTStatus.Done;
    }

    public override void Delete()
    {
        foreach (var item in LTMainMenu.instance.subgoalSpawner.SpawnedInstances)
        {
            item.GetComponentInChildren<LTSubgoal>().Delete();
        }
        base.Delete();
    }
}
