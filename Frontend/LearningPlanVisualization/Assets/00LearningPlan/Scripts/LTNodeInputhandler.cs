using Microsoft.MixedReality.Toolkit.Input;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LTNodeInputhandler : MonoBehaviour, IMixedRealityPointerHandler
{
    Visibility visibility;
    void Start()
    {
        visibility = GetComponent<Visibility>();
    }
    public void OnPointerClicked(MixedRealityPointerEventData eventData)
    {
        visibility.RequirementsVisible = !visibility.RequirementsVisible;
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
