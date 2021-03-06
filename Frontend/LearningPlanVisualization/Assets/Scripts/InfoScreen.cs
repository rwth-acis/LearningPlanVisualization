﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public enum InfoScreenStep { DefineGoal, DefineSubgoals, PlaceSubgoals, SpecifySubgoal, PlaceActions, SpecifyActions }

/// <summary>
/// Information screen, visible while creating a new tree
/// </summary>
public class InfoScreen : MonoBehaviour
{
    public TextMeshPro text;
    public Camera cam;
    private InfoScreenStep step;

    /// <summary>
    /// refference on current creation step
    /// </summary>
    public InfoScreenStep Step
    {
        get { return step; }
        set
        {
            step = value;
            OnChangeInfoScreenStep?.Invoke(step);
        }
    }
    private int numberOfSteps = System.Enum.GetValues(typeof(InfoScreenStep)).Length;

    public TMP_InputField input;
    public TextMeshProUGUI placeholder;
    public GameObject inputField;
    public delegate void ChangeInfoScreenStep(InfoScreenStep newStep);
    public event ChangeInfoScreenStep OnChangeInfoScreenStep;

    public GameObject functionButton;

    private int activeSubgoal;

    private void Awake()
    {
        OnChangeInfoScreenStep += HandleChangeInfosScreenStep;
    }

    // Start is called before the first frame update
    private void Start()
    {
        Step = InfoScreenStep.DefineGoal;
    }


    /// <summary>
    /// set visibilty for buttons and set text if next step is active
    /// </summary>
    /// <param name="newStep"></param>
    private void HandleChangeInfosScreenStep(InfoScreenStep newStep)
    {
        //clear input field
        input.text = "";


        print("Step: " + step);
        switch (newStep)
        {
            case InfoScreenStep.DefineGoal:
                functionButton.SetActive(false);
                inputField.SetActive(true);
                setDefineGoalText();
                break;
            case InfoScreenStep.DefineSubgoals:
                functionButton.SetActive(true);
                inputField.SetActive(true);
                SetDefineSubgoalsText();
                break;
            case InfoScreenStep.PlaceSubgoals:
                functionButton.SetActive(false);
                inputField.SetActive(false);
                SetPlaceSubgoalsText();
                LTMainMenu.instance.SwitchEditMode(true);
                break;
            case InfoScreenStep.SpecifySubgoal:
                functionButton.SetActive(true);
                inputField.SetActive(true);
                SetSpecifySubGoalText(LTMainMenu.instance.subgoalSpawner.SpawnedInstances[activeSubgoal].GetComponentInChildren<LTSubgoal>().title);
                break;
            case InfoScreenStep.PlaceActions:
                functionButton.SetActive(false);
                inputField.SetActive(false);
                SetPlaceActivitiesText(LTMainMenu.instance.subgoalSpawner.SpawnedInstances[activeSubgoal].GetComponentInChildren<LTSubgoal>().title);
                break;
            case InfoScreenStep.SpecifyActions:
                functionButton.SetActive(false);
                inputField.SetActive(false);
                SetSpecifyActionsText();
                break;
            default:
                break;
        }

    }

    /// <summary>
    /// define functionBTN for every step
    /// </summary>
    public void BtnFunctionClicked()
    {
        if (placeholder.enabled) return;
        switch (Step)
        {
            case InfoScreenStep.DefineGoal:
                break;
            case InfoScreenStep.DefineSubgoals:
                LTMainMenu.instance.subgoalSpawner.Spawn();
                var subgoal = LTMainMenu.instance.subgoalSpawner.MostRecentlySpawnedObject.GetComponentInChildren<LTSubgoal>();
                subgoal.Create(input.text, GetSpawnPosition(), LTMainMenu.instance.goalSpawner.MostRecentlySpawnedObject.GetComponentInChildren<LTGoal>());
                input.text = "";
                break;
            case InfoScreenStep.PlaceSubgoals:
                break;
            case InfoScreenStep.SpecifySubgoal:
                LTMainMenu.instance.actionSpawner.Spawn();
                var action = LTMainMenu.instance.actionSpawner.MostRecentlySpawnedObject.GetComponentInChildren<LTAction>();
                action.Create(input.text, GetSpawnPosition(), LTMainMenu.instance.subgoalSpawner.SpawnedInstances[activeSubgoal].GetComponentInChildren<LTSubgoal>());
                input.text = "";
                break;
            case InfoScreenStep.PlaceActions:
                break;
            case InfoScreenStep.SpecifyActions:
                break;
            default:
                break;
        }
    }

    /// <summary>
    /// define nextbutton for every step
    /// </summary>
    public void BtnNextClicked()
    {
        int nextStep = (int)Step;
        switch (Step)
        {
            case InfoScreenStep.DefineGoal:
                if (placeholder.enabled) return;
                LTMainMenu.instance.goalSpawner.Spawn();
                var goal = LTMainMenu.instance.goalSpawner.MostRecentlySpawnedObject.GetComponentInChildren<LTGoal>();
                goal.Create(input.text, GetSpawnPosition());
                LTMainMenu.instance.ChangeGoalMesh();
                break;
            case InfoScreenStep.DefineSubgoals:
                if (LTMainMenu.instance.subgoalSpawner.SpawnedInstances.Length == 0) return;
                break;
            case InfoScreenStep.PlaceSubgoals:
                break;
            case InfoScreenStep.SpecifySubgoal:
                break;
            case InfoScreenStep.PlaceActions:
                activeSubgoal++;
                if (activeSubgoal < LTMainMenu.instance.subgoalSpawner.SpawnedInstances.Length)
                    nextStep = nextStep - 2;
                else
                    activeSubgoal = 0;
                break;
            case InfoScreenStep.SpecifyActions:
                LTMainMenu.instance.ShowCreateNewTree();
                break;
            default:
                break;
        }
        nextStep = (nextStep + 1) % numberOfSteps;

        Step = (InfoScreenStep)nextStep;
    }

    Vector3 GetSpawnPosition()
    {
        return cam.transform.position + cam.transform.forward * 2 - cam.transform.up * 0.1f;
    }
    void setDefineGoalText()
    {
        string infoString = "";
        infoString += "Please name the <b>goal</b> you would like to reach. Try to find a <b>simple term</b> for the goal, that is easy to remember.";
        infoString += "\n<i><size=90%>E.g.: <indent=3em>Juggling</indent></size></i>";
        text.text = infoString;
    }

    void SetDefineSubgoalsText()
    {
        string infoString = "";
        infoString += "As the next step try to <b>define</b> the short-term goals (called <b>subgoals</b>) that are necessary to achieve you long-term goal.";
        infoString += "\n<i><size=90%>E.g.: <indent=3em>Juggling - with balls, with clubs, with rings</indent></size></i>";
        text.text = infoString;
    }

    void SetPlaceSubgoalsText()
    {
        string infoString = "";
        infoString += "Most of the subgoals will have the completion of other subgoals as a <b>precondition</b>.";
        infoString += "\nRearrange the different nodes <b>on the floor</b>, in an order that seems logical for you, beginning with the main goal.";
        infoString += "\nYou can also add preconditions for allready placed nodes by clicking \"connect\".";
        infoString += "\n<i><size=90%>E.g.: <indent=3em>Juggling <- Clubs <- Balls</indent></size></i>";
        infoString += "\n<i><size=90%><indent=3em>Juggling <- Rings <- Balls</indent></size></i>";
        text.text = infoString;
    }

    void SetSpecifySubGoalText(string subgoalName)
    {
        string infoString = "";
        infoString += "Let's get more specific with those subgoals:";
        infoString += "\n\nFor the subgoal <b><u>" + subgoalName + "</u> define</b> the different actions that need to be completed to reach it.";
        infoString += "\nTry to think of <b>SMART</b> activities - <b>S</b>pecific, <b>M</b>easurable, <b>A</b>ttainable, <b>R</b>elevant, <b>T</b>ime based.";
        infoString += "\n<i><size=90%>E.g.: <indent=3em>Juggling with balls - 1 ball, 2 balls, 3 balls, 5 balls</indent></size></i>";
        text.text = infoString;
    }

    void SetPlaceActivitiesText(string subgoalName)
    {
        string infoString = "";
        infoString += "Now please rearrange those <b>activities and place them on top of the subgoal <u>" + subgoalName + "</u></b>, in the same way you considered the preconditions for subgoals.";
        text.text = infoString;
    }

    void SetSpecifyActionsText()
    {
        string infoString = "";
        infoString += "For every action please <b>add more detailed information</b> by editing them:";
        infoString += "<indent=1em>";
        infoString += "\n\u2022 The needed <b>resources</b>";
        infoString += "\n<i><size=90%><indent=2em>E.g.:</indent><indent=5em>workshop, trainer, video, hardware...</indent></size></i>";
        infoString += "\n\u2022 The <b>evidence</b> for completion";
        infoString += "\n<i><size=90%><indent=2em>E.g.:</indent><indent=5em>Juggle 2 minutes without a mistake</indent></size></i>";
        infoString += "\n\u2022 The estimated <b>needed time</b>";
        infoString += "\n<i><size=90%><indent=2em>E.g.:</indent><indent=5em>4 days</indent></size></i>";
        infoString += "</indent>";
        text.text = infoString;
    }


    private void OnDestroy()
    {
        OnChangeInfoScreenStep += HandleChangeInfosScreenStep;
    }
}
