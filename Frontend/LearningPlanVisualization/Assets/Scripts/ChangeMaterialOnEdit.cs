using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Change background of Plates in EditMode
/// </summary>
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
        HandleChangeEditMode(LTMainMenu.instance.editMode);
    }

    public void HandleChangeEditMode(bool editMode)
    {
        if (editMode) meshRenderer.material = editMaterial;
        else meshRenderer.material = normalMaterial;
    }
    private void OnDestroy()
    {
        LTMainMenu.instance.OnChangeEditMode -= HandleChangeEditMode;
    }
}
