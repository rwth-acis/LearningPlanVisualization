using Microsoft.MixedReality.Toolkit.Input;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowGhostsBtn : MonoBehaviour, IMixedRealityPointerHandler
{
    public GameObject gameObjectNode;
    ShowGhosts showGhosts;
    // Start is called before the first frame update
    void Start()
    {
        showGhosts = gameObjectNode.GetComponent<ShowGhosts>();
    }

    public void OnPointerClicked(MixedRealityPointerEventData eventData)
    {
        showGhosts.BtnClicked();
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
