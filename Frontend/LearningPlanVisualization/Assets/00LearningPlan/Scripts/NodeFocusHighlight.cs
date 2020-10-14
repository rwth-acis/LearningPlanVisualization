using Microsoft.MixedReality.Toolkit.Input;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Highlights all selected renderers if the object is focused
/// </summary>
public class NodeFocusHighlight : MonoBehaviour, IMixedRealityFocusHandler
{
    [Tooltip("Renderers which should be affected by the focus highlight")]
    [SerializeField] private Renderer[] targetRenderers;

    /// <summary>
    /// The factor by which the renderers should be darkened if they are focused
    /// </summary>
    public float focusColor;

    private Color[] defaultColors;

    /// <summary>
    /// Checks the component setup and initializes the default color array with the current colors of the renderer's materials
    /// </summary>
    private void Awake()
    {
        defaultColors = new Color[targetRenderers.Length];
    }

    /// <summary>
    /// Called if the object is focused
    /// Sets the color of the targetRenderers to the focusColor
    /// </summary>
    /// <param name="eventData">event data of the focus</param>
    public void OnFocusEnter(FocusEventData eventData)
    {
        for (int i = 0; i < targetRenderers.Length; i++)
        {
            defaultColors[i] = targetRenderers[i].material.color;
        }
        SetColorForAll(focusColor);
    }

    /// <summary>
    /// Called if the object loses focus
    /// Sets teh color of the targetRenderers back to their default colors
    /// </summary>
    /// <param name="eventData"></param>
    public void OnFocusExit(FocusEventData eventData)
    {
        for (int i=0;i<targetRenderers.Length;i++)
        {
            targetRenderers[i].material.color = defaultColors[i];
        }

        GetComponent<LTNodeVisualizer>().MaterialUpdate();
    }

    /// <summary>
    /// Sets the color of the material of all renderers to newColor
    /// </summary>
    /// <param name="newColor">The color to which hte renderers should be set</param>
    private void SetColorForAll(float newColor)
    {
        foreach(Renderer targetRenderer in targetRenderers)
        {
            print(targetRenderer.material.color.ToString()+"->"+(newColor * targetRenderer.material.color).ToString());
            targetRenderer.material.color = newColor * targetRenderer.material.color;

        }
    }
}
