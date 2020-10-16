using Microsoft.MixedReality.Toolkit.Input;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BtnDone : MonoBehaviour, IMixedRealityPointerHandler
{
    public GameObject gameObjectNode;
    LTNode node;
    MeshRenderer nodeMeshRenderer;
    MeshRenderer meshRenderer;

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
        meshRenderer = GetComponent<MeshRenderer>();
        nodeMeshRenderer = node.GetComponent<LTNodeVisualizer>().meshRenderer;
    }

    // Update is called once per frame
    void Update()
    {
        meshRenderer.material = nodeMeshRenderer.material;
    }
}
