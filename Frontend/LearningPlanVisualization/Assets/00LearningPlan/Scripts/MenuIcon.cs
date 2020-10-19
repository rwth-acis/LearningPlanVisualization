using Microsoft.MixedReality.Toolkit.Input;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuIcon : MonoBehaviour, IMixedRealityPointerHandler
{
    public GameObject menuPlate;

    bool menuPlateVisible;
    public bool MenuPlateVisible
    {
        get { return menuPlateVisible; }
        set
        {
            menuPlateVisible = value;
            if (menuPlateVisible) menuPlate.transform.localScale = new Vector3(1, 1, 1);
            else menuPlate.transform.localScale = new Vector3(0, 0, 0);
        }
    }
    public void OnPointerClicked(MixedRealityPointerEventData eventData)
    {
        MenuPlateVisible = !menuPlateVisible;
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
        MenuPlateVisible = false;
    }
}
