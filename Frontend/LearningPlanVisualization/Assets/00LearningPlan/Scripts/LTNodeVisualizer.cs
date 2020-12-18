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

    public TextMeshPro titleText;
    public TextMeshPro detailsTitle;
    public TextMeshPro detailsResources;
    public TextMeshPro detailsEvidence;
    public TextMeshPro detailsTime;
    public TMP_Dropdown detailsChangeResources;

    bool detailsVisible = false;
    LTStatus status;

    public bool DetailsVisible
    {
        get { return detailsVisible; }
        set
        {
            detailsVisible = value;
            UpdateContense();
            if (detailsVisible)
            {
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
        node = GetComponent<LTNode>();
    }

    // Start is called before the first frame update
    void Start()
    {
        UpdateContense();
        MaterialUpdate();
        UpdateResources();
    }

    public void UpdateResources()
    {
        detailsChangeResources.ClearOptions();
        List<string> resources = new List<string>();
        foreach (var resource in LTMainMenu.instance.resources) { resources.Add(resource); }
        detailsChangeResources.AddOptions(resources);
    }

    public void UpdateContense()
    {
        titleText.text = node.GetTitleText();
        if (node.GetType() == typeof(LTAction))
        {
            UpdateResources();
            detailsTitle.text = node.GetTitleText();
            detailsResources.text = node.GetResourcesText();
            detailsEvidence.text = node.GetEvidenceText();
            detailsTime.text = node.GetTimeText();
        }
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

    void Update()
    {
        if (status != node.status)
        {
            status = node.status;
            MaterialUpdate();
        }
    }
}

