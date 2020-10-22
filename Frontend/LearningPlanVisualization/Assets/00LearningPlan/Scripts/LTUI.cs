using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LTUI : MonoBehaviour
{
    public GameObject canvas;

    private void Start()
    {
        LTMainMenu.instance.OnChangeEditMode += HandleChangeEditMode;
    }

    public void HandleChangeEditMode(bool editMode)
    {
        canvas.SetActive(editMode);
    }
}
