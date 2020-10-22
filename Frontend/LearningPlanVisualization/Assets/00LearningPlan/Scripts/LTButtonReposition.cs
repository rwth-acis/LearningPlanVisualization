﻿using Microsoft.MixedReality.Toolkit.Input;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LTButtonReposition : MonoBehaviour, IMixedRealityPointerHandler
{
    public void OnPointerClicked(MixedRealityPointerEventData eventData)
    {
        LTMainMenu.instance.RepositionTree();
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
