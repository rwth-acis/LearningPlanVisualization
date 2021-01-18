using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VisibleInEdite : MonoBehaviour
{
    Vector3 savedScale = Vector3.one;
    public void HandleChangeEditMode(bool editMode)
    {
        if (editMode)
        {
            transform.localScale = savedScale;
        }
        else
        {
            savedScale = transform.localScale;
            transform.localScale = Vector3.zero;
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        LTMainMenu.instance.OnChangeEditMode += HandleChangeEditMode;
        savedScale = transform.localScale;
        HandleChangeEditMode(LTMainMenu.instance.editMode);
    }
    private void OnDestroy()
    {
        LTMainMenu.instance.OnChangeEditMode -= HandleChangeEditMode;
    }
}
