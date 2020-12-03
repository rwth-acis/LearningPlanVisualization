using Microsoft.MixedReality.Toolkit;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InitPlayerspace : MonoBehaviour
{
    public GameObject mixedRealityPlayspace;
    // Start is called before the first frame update
    void Start()
    {
        mixedRealityPlayspace.transform.localPosition = new Vector3(0f, 1f, 0f);
    }
}
