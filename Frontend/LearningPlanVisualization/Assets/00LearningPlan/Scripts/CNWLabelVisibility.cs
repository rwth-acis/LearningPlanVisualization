using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CNWLabelVisibility : MonoBehaviour
{
    public CreateNodeWindow window;

    public bool visibleAsGoal;
    public bool visibleAsSubgoal;
    public bool visibleAsAction;

    private Vector3 scale;
    // Start is called before the first frame update
    void Start()
    {
        window.OnChangeType += HandleChangeType;
        scale = transform.localScale;
    }
    
    void HandleChangeType(LTType type)
    {
        switch (type)
        {
            case LTType.Goal:
                if(visibleAsGoal) transform.localScale = scale;
                else transform.localScale = Vector3.zero;
                break;
            case LTType.Subgoal:
                if (visibleAsSubgoal) transform.localScale = scale;
                else transform.localScale = Vector3.zero;
                break;
            case LTType.Action:
                if (visibleAsAction) transform.localScale = scale;
                else transform.localScale = Vector3.zero;
                break;
            default:
                break;
        }
    }
}
