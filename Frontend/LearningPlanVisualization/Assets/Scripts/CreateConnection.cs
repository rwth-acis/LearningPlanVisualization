using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;


/// <summary>
/// Create connections between Nodes
/// </summary>
public class CreateConnection : MonoBehaviour
{
    public List<Renderer> indicatingRenderer;
    public Color indicatingColor;
    private bool possibleConnection;
    private List<Color> originalColors = new List<Color>();
    private bool creating = false;
    private LTNode startNode;
    public GameObject createConnectionSphere;
    LTNode selfNode;

    // Start is called before the first frame update
    void Start()
    {
        LTMainMenu.instance.OnCreateConnection += HandleCreateConnection;
        LTMainMenu.instance.OnChangeEditMode += HandleChangeEditMode;
        selfNode = GetComponent<LTNode>();
        createConnectionSphere.SetActive(false);
    }

    /// <summary>
    /// Checks if slef is possible connection to "node"
    /// </summary>
    /// <param name="node">start node for a new connection</param>
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

    /// <summary>
    /// start connecting process if node exists
    /// if node == null end connecting
    /// </summary>
    /// <param name="node"></param>
    private void HandleCreateConnection(LTNode node)
    {
        if (node)
            StartConnecting(node);
        else
            EndConnecting();
    }
    
    /// <summary>
    /// On button Click start or cancel the connection process
    /// </summary>
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
