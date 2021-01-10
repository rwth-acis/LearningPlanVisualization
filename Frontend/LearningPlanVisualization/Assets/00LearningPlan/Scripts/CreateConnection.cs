using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CreateConnection : MonoBehaviour
{
    public List<Renderer> indicatingRenderer;
    public Color indicatingColor;
    private bool possibleConnection;
    //private bool haloVisible = false;
    private List<Color> originalColors = new List<Color>();
    private bool creating = false;
    private LTNode startNode;
    //private Behaviour halo;
    public GameObject createConnectionSphere;
    LTNode selfNode;
    // Start is called before the first frame update
    void Start()
    {
        LTMainMenu.instance.OnCreateConnection += HandleCreateConnection;
        LTMainMenu.instance.OnChangeEditMode += HandleChangeEditMode;
        selfNode = GetComponent<LTNode>();
        //halo = (Behaviour)GetComponent("Halo");
        //halo.enabled = false;
        createConnectionSphere.SetActive(false);
    }

    private void IsPossibleConnection(LTNode node)
    {
        possibleConnection = false;
        if (node == selfNode) return;
        if (LTMainMenu.instance.AreConnected(node,selfNode)) return;

        if (node.GetType() == typeof(LTGoal))
        {
            if (selfNode.GetType() == typeof(LTSubgoal) && (selfNode as LTSubgoal).group ==node)
                possibleConnection = true;
        }
        else if (node.GetType() == typeof(LTSubgoal))
        {
            if (selfNode.GetType() == typeof(LTSubgoal)&&(selfNode as LTSubgoal).group==(node as LTSubgoal).group)
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
            if(startNode==selfNode)
                LTMainMenu.instance.InvokeCreateConnection(null);
            else if (possibleConnection)
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

            createConnectionSphere.SetActive(true);
        }
    }
    private void HandleChangeEditMode(bool value)
    {
        EndConnecting();
    }
    private void EndConnecting()
    {
        startNode = null;
        creating = false;
        createConnectionSphere.SetActive(false);
    }
    private void OnDestroy()
    {
        LTMainMenu.instance.OnCreateConnection -= HandleCreateConnection;
        LTMainMenu.instance.OnChangeEditMode -= HandleChangeEditMode;
    }

}
