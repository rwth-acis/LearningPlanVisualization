using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LTSubgoal : LTNode
{
    public int neededActions { get; private set; }
    public int doneActions { get; private set; }
    public List<LTAction> actions { get; private set; }



    public void UpdateActions()
    {
        actions = GetAllRequirements();
        neededActions = 0;
        doneActions = 0;
        foreach(var action in actions)
        {
            if (action.done) doneActions++;
            else neededActions++;
        }

        if (neededActions == 0) status = LTStatus.Done;
        else
        {
            foreach (var node in requirements)
            {
                if (node.GetType() == typeof(LTSubgoal) && !(node.status == LTStatus.Done))
                {
                    status = LTStatus.NotAvailable;
                    return;
                }
            }
            status = LTStatus.Available;
        }
    }
}
