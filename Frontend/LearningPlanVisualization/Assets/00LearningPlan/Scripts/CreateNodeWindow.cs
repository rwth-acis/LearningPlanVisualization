using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateNodeWindow : MonoBehaviour
{

    public delegate void ChangeType(LTType nodeType);
    public event ChangeType OnChangeType;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void HandleChangeType(int nodeType)
    {
        OnChangeType?.Invoke((LTType)nodeType);
    }

}
