using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum LTType { Goal, Subgoal, Action }
public class LTNode : MonoBehaviour
{
    public LTType type = LTType.Goal;
    public string title;
    public List<LTNode> requirements;
    public bool done;
    [Header("Action")]
    public List<string> resources;
    public string evidence;
    public TimeSpan time = TimeSpan.Zero;
}