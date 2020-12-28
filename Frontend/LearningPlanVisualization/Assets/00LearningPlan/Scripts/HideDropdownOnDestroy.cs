using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HideDropdownOnDestroy : MonoBehaviour
{
    public EditResourcesBtn resourcesBtn;
    private void OnDestroy()
    {
        resourcesBtn.DropdownVisibility(false);
    }
}
