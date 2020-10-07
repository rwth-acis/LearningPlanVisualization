using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Visibility : MonoBehaviour
{
    private bool visible = true;
    private bool requirementsVisible = true;

    public bool Visible
    {
        get { return visible; }
        set
        {
            visible = value;
            if (visible) transform.localScale = new Vector3(1, 1, 1);
            else transform.localScale = new Vector3(0, 0, 0);
            RequirementsVisible = visible;
        }
    }
    public bool RequirementsVisible
    {
        get{ return requirementsVisible; }
        set
        {
            requirementsVisible = value;
            foreach (var requirement in GetComponent<LTNode>().requirements)
            {
                requirement.GetComponent<Visibility>().Visible = value;
            }
        }
        
    }
}
