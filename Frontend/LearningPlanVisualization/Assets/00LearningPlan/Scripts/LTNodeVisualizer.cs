using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LTNodeVisualizer : MonoBehaviour
{
    LTNode node;

    private void Awake()
    {
        node = GetComponent<LTNode>();
    }
    // Start is called before the first frame update
    void Start()
    {
        var text = GetComponentInChildren<Text>();
        text.text = node.title;
    }
}
