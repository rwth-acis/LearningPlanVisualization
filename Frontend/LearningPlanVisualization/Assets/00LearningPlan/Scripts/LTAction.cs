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
