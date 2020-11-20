﻿using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CreateConnection : MonoBehaviour
{
    public List<Renderer> indicatingRenderer;
    public Color indicatingColor;
    private bool possibleConnection;
    private bool blinkVisible = true;
    private List<Color> originalColors = new List<Color>();
    private bool creating = false;
    private LTNode startNode;
    LTNode selfNode;
    // Start is called before the first frame update
    void Start()
    {
        LTMainMenu.instance.OnCreateConnection += HandleCreateConnection;
        selfNode = GetComponent<LTNode>();
    }

    private void IsPossibleConnection(LTNode node)
    {
        possibleConnection = false;
        if (node == selfNode) return;
        if (LTMainMenu.instance.AreConnected(node,selfNode)) return;

        if (node.GetType() == typeof(LTGoal))
        {
            if (selfNode.GetType() == typeof(LTSubgoal))
                possibleConnection = true;
        }
        else if (node.GetType() == typeof(LTSubgoal))
        {
            if (selfNode.GetType() == typeof(LTSubgoal))
                possibleConnection = true;
            else if (selfNode.GetType() == typeof(LTAction) && (selfNode as LTAction).group==node)
                possibleConnection = true;
        }
        else if (node.GetType() == typeof(LTAction))
        {
            if (selfNode.GetType() == typeof(LTAction) && (selfNode as LTAction).group == (node as LTAction).group)
                possibleConnection = true;
        }
    }

    private void HandleCreateConnection(LTNode node)
    {
        if (node)
            StartConnecting(node);
        else
            EndConnecting();
    }
    
    public void ConnectionBtnClicked()
    {
        if (creating)
        {
            if (possibleConnection)
            {
                LTMainMenu.instance.NewConnection(startNode,selfNode);
                LTMainMenu.instance.InvokeCreateConnection(null);
            }
        }
        else LTMainMenu.instance.InvokeCreateConnection(selfNode);
    }

    private void StartConnecting(LTNode node)
    {
        startNode = node;
        creating = true;
        IsPossibleConnection(node);
        if (possibleConnection)
        {
            originalColors.Clear();
            foreach (var renderer in indicatingRenderer)
            {
                originalColors.Add(renderer.material.color);
            }
            InvokeRepeating("Blink", 0, 0.5f);
        }
    }

    private void EndConnecting()
    {
        startNode = null;
        creating = false;
        CancelInvoke("Blink");
        blinkVisible = true;
        if (possibleConnection)
        {
            var index = 0;
            foreach (var renderer in indicatingRenderer)
            {
                renderer.material.color = originalColors[index];
                index++;
            }
        }
    }


    void Blink()
    {
        blinkVisible = !blinkVisible;
        var index = 0;
        foreach (var renderer in indicatingRenderer)
        {
            renderer.material.color = blinkVisible ? originalColors[index] : indicatingColor;
            index++;
        }
    }
}
