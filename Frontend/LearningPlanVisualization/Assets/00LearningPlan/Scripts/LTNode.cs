﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;


public enum LTStatus { Done, Available, NotAvailable }

abstract public class LTNode : MonoBehaviour
{
    public string title;
    public List<LTNode> requirements;
    public LTStatus status { get; protected set; }
    public int level;

    virtual public string GetDetailsText()
    {
        return "NOT IMPLEMENTED";
    }

    public void Create(string newtitle)
    {
        title = newtitle;
        name = newtitle;
    }
    public void Create(string newtitle,Vector3 position)
    {
        Create(newtitle);
        transform.position = position;
    }

    public void ResetLevel()
    {
        level = 0;
        foreach (var requirement in requirements)
        {
            requirement.ResetLevel();
        }
    }
    public void CalculateLevel(int lastLevel)
    {
        if (level <= lastLevel) level = lastLevel + 1;
        foreach(var requirement in requirements)
        {
            requirement.CalculateLevel(level);
        }
    }

    abstract public void RepositionRequirements(float margin);

    public List<LTAction> GetAllRequirements()
    {
        List<LTAction> returnList = new List<LTAction>();
        List<LTAction> candidates;
        if (requirements.Count == 0) return returnList;
        foreach(var requirement in requirements)
        {
            if (requirement.GetType() == typeof(LTAction))
            {
                candidates = requirement.GetAllRequirements();
                foreach(var candidate in candidates)
                {
                    if (!returnList.Contains(candidate)) returnList.Add(candidate);
                }
                if (!returnList.Contains(requirement)) returnList.Add(requirement as LTAction);
            }
        }
        return returnList;
    }
}