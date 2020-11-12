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

        private NonNativeKeyboard keyboard = null;
        private void Start()
        {
            keyboard = LTMainMenu.instance.keyboard;
        }
        public void OnPointerDown(PointerEventData eventData)
        {
            keyboard.RepositionKeyboard(transform, aCollider,0.5f);
            keyboard.PresentKeyboard();

            keyboard.OnClosed += DisableKeyboard;
            keyboard.OnTextSubmitted += DisableKeyboard;
            keyboard.OnTextUpdated += UpdateText;
        }

        private void UpdateText(string text)
        {
            GetComponent<TMP_InputField>().text = text;
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