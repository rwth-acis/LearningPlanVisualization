using Microsoft.MixedReality.Toolkit.Input;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuIcon : MonoBehaviour
{
    public GameObject menuPlate;
    private Vector3 savedScale;
    bool menuPlateVisible;
    float time;

    public bool MenuPlateVisible
    {
        get { return menuPlateVisible; }
        set
        {
            menuPlateVisible = value;
            if (menuPlateVisible) menuPlate.transform.localScale = savedScale;
            else menuPlate.transform.localScale = Vector3.zero;
        }
    }

    public void OnClickStart()
    {
        time = Time.time * 1000;
    }
    public void OnClickEnd()
    {
        if (Time.time * 1000 - time < 200)
        {
            MenuPlateVisible = !menuPlateVisible;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        savedScale = menuPlate.transform.localScale;
        MenuPlateVisible = false;

    }
}
