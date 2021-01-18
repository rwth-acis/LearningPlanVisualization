using Microsoft.MixedReality.Toolkit.Input;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// enlarge plate on focus enter and revert on exit
/// </summary>
public class EnlargeOnFocus : MonoBehaviour, IMixedRealityFocusHandler
{
    public float factor = 2.0f;
    public void OnFocusEnter(FocusEventData eventData)
    {
        transform.localScale *= factor;
    }

    public void OnFocusExit(FocusEventData eventData)
    {
        transform.localScale *= 1 / factor;
    }
}
