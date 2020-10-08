using Microsoft.MixedReality.Toolkit.Input;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LTNodeInputhandler : MonoBehaviour, IMixedRealityPointerHandler
{
    Visibility visibility;
    LTNode node;
    void Start()
    {
        visibility = GetComponent<Visibility>();
        node = GetComponent<LTNode>();
    }
    public void OnPointerClicked(MixedRealityPointerEventData eventData)
    {
        if (node.GetType() == typeof(LTGoal)){

        }
        if (node.GetType() == typeof(LTSubgoal)){
            visibility.RequirementsVisible = !visibility.RequirementsVisible;
        }
        if (node.GetType() == typeof(LTAction)){

        }

    }

    public void OnPointerDown(MixedRealityPointerEventData eventData)
    {
    }

    public void OnPointerDragged(MixedRealityPointerEventData eventData)
    {
    }

    public void OnPointerUp(MixedRealityPointerEventData eventData)
    {
    }
}
