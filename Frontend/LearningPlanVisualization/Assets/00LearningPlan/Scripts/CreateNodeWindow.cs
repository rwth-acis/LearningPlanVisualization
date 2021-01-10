using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateNodeWindow : MonoBehaviour
{

    public delegate void ChangeType(LTType nodeType);
    public event ChangeType OnChangeType;

    public void HandleChangeType(int nodeType)
    {
        OnChangeType?.Invoke((LTType)nodeType);
    }

}
