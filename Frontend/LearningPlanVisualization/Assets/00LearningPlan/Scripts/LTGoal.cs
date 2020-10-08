using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LTGoal : LTNode
{
    public override void RepositionRequirements(float margin)
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
