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
            if (visible) transform.localScale = Vector3.one;
            else transform.localScale = Vector3.zero;
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
                if (requirement.GetType() == typeof(LTAction))
                {
                    requirement.GetComponent<Visibility>().Visible = value;
                }
            }
        }
        
    }
}
