using Microsoft.MixedReality.Toolkit.Input;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// On Click Switch Status of Node between Done/NotDone
/// </summary>
public class BtnDone : MonoBehaviour, IMixedRealityPointerHandler
{
    public GameObject gameObjectNode;
    LTNode node;
    MeshRenderer nodeMeshRenderer;
    MeshRenderer meshRenderer;
    LTNodeVisualizer visualizer;

    public void OnPointerClicked(MixedRealityPointerEventData eventData)
    {
        node.BtnDoneClicked();
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

    // Start is called before the first frame update
    void Start()
    {
        node = gameObjectNode.GetComponent<LTNode>();
        visualizer = gameObjectNode.GetComponent<LTNodeVisualizer>();
        meshRenderer = GetComponent<MeshRenderer>();
        nodeMeshRenderer = node.GetComponent<LTNodeVisualizer>().meshRenderer;

        visualizer.OnChangeMaterial += HandleChangeMaterial;
    }


   /// <summary>
   /// Sync material of button to material of node
   /// </summary>
    public void HandleChangeMaterial()
    {
        meshRenderer.material = nodeMeshRenderer.material;
    }

    private void OnDestroy()
    {
        visualizer.OnChangeMaterial -= HandleChangeMaterial;
    }
}
