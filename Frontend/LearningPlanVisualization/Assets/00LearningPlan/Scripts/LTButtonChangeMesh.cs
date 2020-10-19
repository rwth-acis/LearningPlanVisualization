using Microsoft.MixedReality.Toolkit.Input;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LTButtonChangeMesh : MonoBehaviour, IMixedRealityPointerHandler
{
    public GameObject menuPlate;
    LTMainMenu menu;

    public void OnPointerClicked(MixedRealityPointerEventData eventData)
    {
        menu.ChangeGoalMesh();
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
        menu = menuPlate.GetComponent<LTMainMenu>();
    }

    // Update is called once per frame
    void Update()
    {

    }
}
