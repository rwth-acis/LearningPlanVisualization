using Microsoft.MixedReality.Toolkit.UI;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;


public enum LTStatus { Done, Available, NotAvailable }
public enum LTType { Goal, Subgoal, Action}

abstract public class LTNode : MonoBehaviour
{
    public int id;
    public string title;
    public List<LTNode> requirements;
    public LTStatus status=LTStatus.NotAvailable;
    public LTStatus calendarStatus = LTStatus.NotAvailable;
    public int level;
    public GameObject self;
    public GameObject addToCalendarSphere;
    protected LTNodeVisualizer visualizer;
    protected Visibility visibility;
    float clickTime;
    ManipulationHandler manipulationHandler;
    Transform noTransform;


    public delegate void ChangePosition();
    public event ChangePosition OnChangePosition;


    private void Awake()
    {
        LTMainMenu.instance.OnChangeEditMode += HandleChangeEditMode;
        LTMainMenu.instance.OnChangeMode += HandleChangeMode;
        manipulationHandler = GetComponent<ManipulationHandler>();
        noTransform = manipulationHandler.HostTransform;
        HandleChangeEditMode(LTMainMenu.instance.editMode);
        addToCalendarSphere.SetActive(false);
        visualizer = GetComponent<LTNodeVisualizer>();
        visibility = GetComponent<Visibility>();
        id = LTMainMenu.instance.nextId;
        LTMainMenu.instance.nextId++;
    }

    virtual public void HandleChangeEditMode(bool editMode)
    {
        if (editMode)
        {
            manipulationHandler.HostTransform = transform.parent;

        }
        else
        {
            manipulationHandler.HostTransform = noTransform;
        }
    }

    /// <summary>
    /// Returns the corresponding Text When implemented in SubClass
    /// </summary>
    /// <returns></returns>
    virtual public string GetDetailsText()
    {
        return "NOT IMPLEMENTED";
    }

    /// <summary>
    /// Returns the corresponding Text When implemented in SubClass
    /// </summary>
    /// <returns></returns>
    virtual public string GetEvidenceText()
    {
        return "NOT IMPLEMENTED";
    }

    /// <summary>
    /// Returns the corresponding Text When implemented in SubClass
    /// </summary>
    /// <returns></returns>
    virtual public string GetTimeText()
    {
        return "NOT IMPLEMENTED";
    }

    /// <summary>
    /// Returns the corresponding Text When implemented in SubClass
    /// </summary>
    /// <returns></returns>
    virtual public string GetResourcesText()
    {
        return "NOT IMPLEMENTED";
    }


    virtual public void EditText(LTEditText.EditType editType, string text)
    {

    }

    virtual public void BtnDoneClicked()
    {
        print("BTNDoneClicked");
    }
    public void OnClickStart()
    {
        clickTime = Time.time * 1000;
    }
    public void OnClickEnd()
    {
        if (Time.time * 1000 - clickTime < 200)
        {
            NodeClicked();
        }
    }

    virtual public void NodeClicked()
    {
    }

    virtual public string GetTitleText()
    {
        return title;
    }

    virtual public void UpdateStatus()
    {
    }
    virtual public void UpdateCalendarStatus()
    {
    }
    virtual public void HandleChangeMode(LTModes oldMode, LTModes newModa)
    {
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

    void Update()
    {
        if (transform.hasChanged)
        {
            transform.hasChanged = false;
            OnChangePosition?.Invoke();
        }
        UpdateStatus();
    }

    public List<LTConnection> GetAllConnections()
    {
        List<LTConnection> allConnections = new List<LTConnection>();
        foreach (var item in LTMainMenu.instance.connectionSpawner.SpawnedInstances)
        {
            var connection = item.GetComponent<LTConnection>();
            if (this == connection.start || this == connection.end)
            {
                allConnections.Add(connection);
            }
        }
        return allConnections;
    }

    virtual public void Delete()
    {
        var allConnections = GetAllConnections();
        foreach (var connection in allConnections)
        {
            connection.Delete();
        }
        Destroy(self);
    }

    protected virtual void OnDestroy()
    {
        LTMainMenu.instance.OnChangeEditMode -= HandleChangeEditMode;

        LTMainMenu.instance.OnChangeMode -= HandleChangeMode;
    }
}