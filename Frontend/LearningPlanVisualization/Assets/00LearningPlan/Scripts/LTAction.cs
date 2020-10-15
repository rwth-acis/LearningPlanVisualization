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


    public override string GetDetailsText()
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


        /*
            < u >< size = 140 %>< align = "center" > test title </ align ></ size ></ u >

           Resources:
< indent = 2em > test a d sadfdsf
     gfsg hdg h dghgh hdfh hdfh hdfgh hfdghdf </ indent >
     Evidence:
< indent = 2em > test a d sadfdsf gfsg hdg h dghgh hdfh hdfh hdfgh hfdghdf </ indent >
     Time:
< indent = 2em > test stunden </ indent >
*/

        return returnText;
    }

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

    public void Create(string newTitle, Vector3 newPosition, bool newDone, List<string> newResources, string newEvidence, TimeSpan newTime)
    {
        Create(newTitle, newPosition);
        done = newDone;
        resources = newResources;
        evidence = newEvidence;
        time = newTime;
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
