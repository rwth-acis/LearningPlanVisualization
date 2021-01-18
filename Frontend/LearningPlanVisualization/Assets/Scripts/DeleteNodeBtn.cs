using Microsoft.MixedReality.Toolkit.Input;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeleteNodeBtn : MonoBehaviour, IMixedRealityPointerHandler
{
    public GameObject gameObjectNode;
    LTNode node;
    // Start is called before the first frame update
    void Start()
    {
        node = gameObjectNode.GetComponent<LTNode>();
    }
    public void OnPointerClicked(MixedRealityPointerEventData eventData)
    {
        node.Delete();
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
