using Microsoft.MixedReality.Toolkit.Utilities;
using System;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Microsoft.MixedReality.Toolkit.Experimental.UI
{
    /// <summary>
    /// This component links the NonNativeKeyboard to a TMP_InputField
    /// </summary>
    [RequireComponent(typeof(TMP_InputField))]
    public class LTInputField : MonoBehaviour, IPointerDownHandler
    {
        public BoxCollider aCollider;
        private TMP_InputField outputText;
        private NonNativeKeyboard keyboard = null;
        private void Start()
        {
            keyboard = LTMainMenu.instance.keyboard;
            outputText = GetComponent<TMP_InputField>();
        }
        public void OnPointerDown(PointerEventData eventData)
        {
            LTMainMenu.instance.RepositionKeyboard();
            keyboard.PresentKeyboard(outputText.text);

            keyboard.OnClosed += DisableKeyboard;
            keyboard.OnTextSubmitted += DisableKeyboard;
            keyboard.OnTextUpdated += UpdateText;
        }

        private void UpdateText(string text)
        {
            outputText.text = text;
        }

        private void DisableKeyboard(object sender, EventArgs e)
        {
            keyboard.OnTextUpdated -= UpdateText;
            keyboard.OnClosed -= DisableKeyboard;
            keyboard.OnTextSubmitted -= DisableKeyboard;

            keyboard.Close();
        }
    }
}