using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LTGhostConnection : MonoBehaviour
{
    public LTNode start;
    public GameObject end;
    public GameObject cylinder;
    private Vector3 startPosition;
    private Vector3 endPosition;
    public Renderer mainRenderer;
    private Visibility startVisibility;
    private Visibility endVisibility;
    private float length;
    private Vector3 offset;
    private LTType lTType;


    public void Create(LTNode newStart, Vector3 newOffset, LTType newLTType)
    {
        start = newStart;
        offset = newOffset;
        lTType = newLTType;

        startVisibility = start.GetComponent<Visibility>();
        start.OnChangePosition += HandleChangePosition;
        LTMainMenu.instance.OnDestroyGhosts += HandleDestroyGhosts;

        cylinder.transform.up = offset;
        length = Vector3.Magnitude(offset);
        mainRenderer.material.mainTextureScale = new Vector2(2f, 6f * length);
        cylinder.transform.localScale = new Vector3(1, length, 1);

        HandleChangePosition();
    }

    void HandleChangePosition()
    {
        startPosition = start.transform.position;
        end.transform.position = startPosition + offset;
        cylinder.transform.position = startPosition + (offset * 0.5f);
    }

    void HandleDestroyGhosts()
    {
        Destroy(gameObject);
    }

    public void OnClick()
    {
        //Create Node
        switch (lTType)
        {
            case LTType.Goal:
                break;
            case LTType.Subgoal:
                LTMainMenu.instance.subgoalSpawner.Spawn();
                var subgoal = LTMainMenu.instance.subgoalSpawner.MostRecentlySpawnedObject.GetComponentInChildren<LTSubgoal>();
                if (start.GetType() == typeof(LTSubgoal))
                {
                    subgoal.Create("Set Name", end.transform.position, (start as LTSubgoal).group);
                }
                else
                {
                    subgoal.Create("Set Name", end.transform.position, start as LTGoal);
                }
                //Create Connection
                LTMainMenu.instance.NewConnection(start, subgoal);
                break;
            case LTType.Action:
                LTMainMenu.instance.actionSpawner.Spawn();
                var action = LTMainMenu.instance.actionSpawner.MostRecentlySpawnedObject.GetComponentInChildren<LTAction>();
                if(start.GetType() == typeof(LTAction))
                {
                    action.Create("Set Name", end.transform.position, (start as LTAction).group);
                }
                else
                {
                    action.Create("Set Name", end.transform.position, start as LTSubgoal);
                }
                //Create Connection
                LTMainMenu.instance.NewConnection(start, action);
                break;
            default:
                break;
        }


        //DestroyGhosts
        LTMainMenu.instance.InvokeDestroyGhosts();

    }


    private void OnDestroy()
    {
        start.OnChangePosition -= HandleChangePosition;
        LTMainMenu.instance.OnDestroyGhosts -= HandleDestroyGhosts;
    }
}
