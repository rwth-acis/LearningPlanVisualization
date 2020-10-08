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


    public override void RepositionRequirements(float margin)
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



    // Start is called before the first frame update
    void Start()
    {
        
    }
    // Update is called once per frame
    void Update()
    {
        if (done) status = LTStatus.Done;
        else
        {
            foreach (var node in requirements)
            {
                if (!(node.status==LTStatus.Done))
                {
                    status = LTStatus.NotAvailable;
                    return;
                }
            }
            status = LTStatus.Available;
        }
    }
}
