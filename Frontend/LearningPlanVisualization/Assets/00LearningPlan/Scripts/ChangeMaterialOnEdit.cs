using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeMaterialOnEdit : MonoBehaviour
{
    public Material editMaterial;
    public MeshRenderer meshRenderer;
    private Material normalMaterial;
    // Start is called before the first frame update
    void Start()
    {
        LTMainMenu.instance.OnChangeEditMode += HandleChangeEditMode;
        normalMaterial = meshRenderer.material;
    }

    public void HandleChangeEditMode(bool editMode)
    {
        if (editMode) meshRenderer.material = editMaterial;
        else meshRenderer.material = normalMaterial;
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
