using Microsoft.MixedReality.Toolkit.Experimental.UI;
using Microsoft.MixedReality.Toolkit.Input;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using System;

public class LTEditText : MonoBehaviour, IMixedRealityPointerHandler
{
    public enum EditType { Title, Evidence, Time }
    public TextMeshPro textField;
    public BoxCollider aCollider;
    public EditType editType;
    public LTNode node;
    private NonNativeKeyboard keyboard = null;

    private void Start()
    {
        keyboard = LTMainMenu.instance.keyboard;
    }

    public void OnPointerClicked(MixedRealityPointerEventData eventData)
    {
        LTMainMenu.instance.RepositionKeyboard();
        if (editType == EditType.Time)
        {
            keyboard.PresentKeyboard(textField.text.Substring(0, textField.text.IndexOf(" ")),NonNativeKeyboard.LayoutType.Symbol);
        }
        else
        {
            keyboard.PresentKeyboard(textField.text);
        }
        
        keyboard.OnClosed += DisableKeyboard;
        keyboard.OnTextSubmitted += DisableKeyboard;
        keyboard.OnTextUpdated += UpdateText;
    }

    private void UpdateText(string text)
    {
        textField.text = text;
    }

    private void DisableKeyboard(object sender, EventArgs e)
    {
        keyboard.OnTextUpdated -= UpdateText;
        keyboard.OnClosed -= DisableKeyboard;
        keyboard.OnTextSubmitted -= DisableKeyboard;

        keyboard.Close();
        node.EditText(editType, textField.text);
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
