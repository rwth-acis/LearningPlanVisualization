using Microsoft.MixedReality.Toolkit.Input;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// On button Click start or cancel the connection process
/// </summary>
public class CreateConnectionBtn : MonoBehaviour, IMixedRealityPointerHandler
{
    public GameObject gameObjectNode;
    CreateConnection createConnection;
    // Start is called before the first frame update
    void Start()
    {
        createConnection = gameObjectNode.GetComponent<CreateConnection>();
    }

    public void OnPointerClicked(MixedRealityPointerEventData eventData)
    {
        createConnection.ConnectionBtnClicked();
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
