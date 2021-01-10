using Microsoft.MixedReality.Toolkit.Input;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
