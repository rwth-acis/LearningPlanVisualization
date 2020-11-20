using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Visibility : MonoBehaviour
{
    private bool visible = true;
    private bool requirementsVisible = true;
    private Vector3 storedLocalScale = Vector3.one;

    public bool Visible
    {
        get { return visible; }
        set
        {
            visible = value;
            if (transform.localScale != Vector3.zero) storedLocalScale = transform.localScale;
            if (visible) transform.localScale = storedLocalScale;
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
