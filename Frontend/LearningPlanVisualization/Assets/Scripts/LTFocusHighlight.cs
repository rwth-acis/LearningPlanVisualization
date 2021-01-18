using Microsoft.MixedReality.Toolkit.Input;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LTFocusHighlight : MonoBehaviour, IMixedRealityFocusHandler
{
    [SerializeField] public  Renderer targetRenderer;

    /// <summary>
    /// The factor by which the renderers should be darkened if they are focused
    /// </summary>
    public float focusFactor;

    private Color defaultColor;


    public void OnFocusEnter(FocusEventData eventData)
    {
        defaultColor = targetRenderer.material.color;
        targetRenderer.material.color = focusFactor * targetRenderer.material.color;
    }
    
    public void OnFocusExit(FocusEventData eventData)
    {
        targetRenderer.material.color = defaultColor;
    }
}
