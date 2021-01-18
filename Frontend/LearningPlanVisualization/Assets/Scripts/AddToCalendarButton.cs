using Microsoft.MixedReality.Toolkit.Input;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Atached to a button (Halo) of a Node to Add it to the calendar on click
/// </summary>
public class AddToCalendarButton : MonoBehaviour, IMixedRealityPointerHandler
{
    public LTNode node;
    public void OnPointerClicked(MixedRealityPointerEventData eventData)
    {
        LTMainMenu.instance.calendar.AddToCalendarButtonClicked(node as LTAction);
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
