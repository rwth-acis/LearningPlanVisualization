using Microsoft.MixedReality.Toolkit.Experimental.UI;
using Microsoft.MixedReality.Toolkit.Input;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class EditResourcesBtn : MonoBehaviour, IMixedRealityPointerHandler
{
    public GameObject resourcesCanvas;
    Vector3 savedSize=Vector3.one;
    TMP_Dropdown dropDown;
    private NonNativeKeyboard keyboard = null;
    public LTNodeVisualizer visualizer;
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
        keyboard = LTMainMenu.instance.keyboard;
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

    public void ShowKeyboard()
    {
        print("resourcebtn.ShowKeyboard");
        LTMainMenu.instance.RepositionKeyboard();
        keyboard.PresentKeyboard();
        keyboard.OnClosed += CloseKeyboard;
        keyboard.OnTextSubmitted += SubmitKeyboard;
    }

    private void CloseKeyboard(object sender, EventArgs e)
    {
        DisableKeyboard(sender, e);
    }

    private void SubmitKeyboard(object sender, EventArgs e)
    {
        AddResource(keyboard.GetComponentInChildren<TMP_InputField>().text);
        DisableKeyboard(sender, e);
    }

    private void DisableKeyboard(object sender, EventArgs e)
    {
        keyboard.OnClosed -= CloseKeyboard;
        keyboard.OnTextSubmitted -= SubmitKeyboard;
        keyboard.Close();
    }
    
    private void AddResource(string newResource)
    {
        if (String.IsNullOrWhiteSpace(newResource)) return;
        LTMainMenu.instance.resources.Add(newResource);
        visualizer.UpdateResources();
        print(newResource+" added as new Resource");
    }
}
