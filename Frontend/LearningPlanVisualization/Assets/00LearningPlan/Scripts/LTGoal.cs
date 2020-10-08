using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LTGoal : LTNode
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
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
}
