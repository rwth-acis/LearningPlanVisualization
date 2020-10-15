using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class LTNodeVisualizer : MonoBehaviour
{
    LTNode node;
    public Material materialDone;
    public Material materialAvailable;
    public Material materialNotAvailable;
    public MeshRenderer meshRenderer;
    public GameObject titlePlate;
    public GameObject detailsPlate;

    TextMeshPro titleText;
    TextMeshPro detailsText;
    bool detailsVisible = false;

    public bool DetailsVisible
    {
        get { return detailsVisible; }
        set
        {
            detailsVisible = value;
            if (detailsVisible)
            {
                detailsText.text = node.GetDetailsText();
                detailsPlate.transform.localScale = new Vector3(1,1,1);
                titlePlate.transform.localScale = new Vector3(0,0,0);
            }
            else
            {
                detailsPlate.transform.localScale = new Vector3(0,0,0);
                titlePlate.transform.localScale = new Vector3(1,1,1);
            }
            
        }
    }

    void Awake()
    {
        titleText = titlePlate.GetComponentInChildren<TextMeshPro>();
        detailsText = detailsPlate.GetComponentInChildren<TextMeshPro>();
        node = GetComponent<LTNode>();
    }

    // Start is called before the first frame update
    void Start()
    {
       titleText.text = node.title;
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

