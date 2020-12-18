using Microsoft.MixedReality.Toolkit.Input;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class EditResourcesBtn : MonoBehaviour, IMixedRealityPointerHandler
{
    public GameObject resourcesCanvas;
    Vector3 savedSize=Vector3.one;
    TMP_Dropdown dropDown;
    public void OnPointerClicked(MixedRealityPointerEventData eventData)
    {
        dropDown.Show();
        DropdownVisibility(dropDown.IsExpanded);
            
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

    private void Start()
    {
        dropDown = resourcesCanvas.GetComponentInChildren<TMP_Dropdown>();
    }
    public void DropdownVisibility(bool value)
    {
        if (value)
        {
            if (resourcesCanvas.transform.localScale == Vector3.zero)
            {
                resourcesCanvas.transform.localScale = savedSize;
            }
        }
        else
        {
            if (resourcesCanvas.transform.localScale != Vector3.zero)
            {
                savedSize = resourcesCanvas.transform.localScale;
                resourcesCanvas.transform.localScale = Vector3.zero;
            }
        }
    }
}
