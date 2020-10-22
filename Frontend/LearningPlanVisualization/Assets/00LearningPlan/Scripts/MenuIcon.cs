using Microsoft.MixedReality.Toolkit.Input;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuIcon : MonoBehaviour
{
    public GameObject menuPlate;

    bool menuPlateVisible;
    float time;

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
        MenuPlateVisible = false;
    }
}
