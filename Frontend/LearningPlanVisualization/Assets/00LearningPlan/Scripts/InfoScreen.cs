using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public enum InfoScreenStep { DefineGoal, DefineSubgoals, PlaceSubgoals, SpecifySubgoal, PlaceActivites, SpecifyActions }
public class InfoScreen : MonoBehaviour
{
    public TextMeshPro text;
    private InfoScreenStep step;
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

    public TextMeshProUGUI input;
    public TextMeshProUGUI placeholder;
    public delegate void ChangeInfoScreenStep(InfoScreenStep newStep);
    public event ChangeInfoScreenStep OnChangeInfoScreenStep;

    // Start is called before the first frame update
    private void Start()
    {
        Step = InfoScreenStep.DefineGoal;
        OnChangeInfoScreenStep += HandleChangeInfosScreenStep;
    }

    private void HandleChangeInfosScreenStep(InfoScreenStep newStep)
    {
        //clear input field

        switch (newStep)
        {
            case InfoScreenStep.DefineGoal:
                setDefineGoalText();
                break;
            case InfoScreenStep.DefineSubgoals:
                SetDefineSubgoalsText();
                break;
            case InfoScreenStep.PlaceSubgoals:
                SetPlaceSubgoalsText();
                break;
            case InfoScreenStep.SpecifySubgoal:
                SetSpecifySubGoalText("Dummy Subgoal");
                break;
            case InfoScreenStep.PlaceActivites:
                SetPlaceActivitiesText("Dummy Subgoal");
                break;
            case InfoScreenStep.SpecifyActions:
                SetSpecifyActionsText();
                break;
            default:
                break;
        }

    }

    public void BtnFunktionClicked()
    {
        if (placeholder.enabled) return;
        switch (Step)
        {
            case InfoScreenStep.DefineGoal:
                break;
            case InfoScreenStep.DefineSubgoals:
                break;
            case InfoScreenStep.PlaceSubgoals:
                break;
            case InfoScreenStep.SpecifySubgoal:
                break;
            case InfoScreenStep.PlaceActivites:
                break;
            case InfoScreenStep.SpecifyActions:
                break;
            default:
                break;
        }
    }

    public void BtnNextClicked()
    {
        if (placeholder.enabled) return;
        switch (Step)
        {
            case InfoScreenStep.DefineGoal:
                break;
            case InfoScreenStep.DefineSubgoals:
                break;
            case InfoScreenStep.PlaceSubgoals:
                break;
            case InfoScreenStep.SpecifySubgoal:
                break;
            case InfoScreenStep.PlaceActivites:
                break;
            case InfoScreenStep.SpecifyActions:
                break;
            default:
                break;
        }

        int nextStep = ((int)Step + 1) % numberOfSteps;
        Step = (InfoScreenStep)nextStep;
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
        infoString += "\nTake the different nodes <b>out of your tash and place them on the floor</b>, in an order that seems logical for you, beginning with the main goal.";
        infoString += "\nYou can also add preconditions for allready placed nodes.";
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
        infoString += "Now please take those <b>activities out of your stash and place them on top of the subgoal <u>" + subgoalName + "</u></b>, in the same way you considered the preconditions for subgoals.";
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
}
