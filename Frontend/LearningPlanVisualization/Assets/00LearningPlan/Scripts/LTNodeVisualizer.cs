using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LTNodeVisualizer : MonoBehaviour
{
    LTNode node;
    Material materialDone;
    Material materialAvailable;
    Material materialNotAvailable;
    MeshRenderer meshRenderer;

    void Awake()
    {

    }

    // Start is called before the first frame update
    void Start()
    {
        node = GetComponent<LTNode>();
        materialDone = Resources.Load<Material>("Materials/NodeDone");
        materialAvailable = Resources.Load<Material>("Materials/NodeAvailable");
        materialNotAvailable = Resources.Load<Material>("Materials/NodeNotAvailable");
        meshRenderer = GetComponentInChildren<MeshRenderer>();

        var text = GetComponentInChildren<Text>();
        text.text = node.title;
    }

    public void MaterialUpdate()
    {
        switch (node.status)
        {
            case LTStatus.Done:
                    meshRenderer.material = materialDone;
                break;
            case LTStatus.Available:
                meshRenderer.material = materialAvailable;
                break;
            case LTStatus.NotAvailable:
                meshRenderer.material = materialNotAvailable;
                break;
        }
    }
}

