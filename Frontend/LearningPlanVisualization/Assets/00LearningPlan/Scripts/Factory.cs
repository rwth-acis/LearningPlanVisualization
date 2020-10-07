using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Factory : MonoBehaviour
{
    public GameObject prefabLTNode;
    public GameObject prefabLTConnection;
    public GameObject goal;
    private void Awake()
    {

    }
    // Start is called before the first frame update
    void Start()
    {
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Keypad0))
        {

            goal = Instantiate(prefabLTNode, new Vector3(0, 0, 10), Quaternion.identity);
            var LTNgoal = goal.GetComponentInChildren<LTNode>();

            var subgoal1 = Instantiate(prefabLTNode, new Vector3(0, 0, 4), Quaternion.identity);
            var LTNsubgoal1 = subgoal1.GetComponentInChildren<LTNode>();
            var subgoal2 = Instantiate(prefabLTNode, new Vector3(3, 0, 7), Quaternion.identity);
            var LTNsubgoal2 = subgoal2.GetComponentInChildren<LTNode>();
            var subgoal3 = Instantiate(prefabLTNode, new Vector3(-3, 0, 7), Quaternion.identity);
            var LTNsubgoal3 = subgoal3.GetComponentInChildren<LTNode>();
            

            LTNgoal.Create(LTType.Goal, "Juggling");
            LTNgoal.requirements.Add(LTNsubgoal2);
            LTNgoal.requirements.Add(LTNsubgoal3);

            LTNsubgoal1.Create(LTType.Subgoal, "Balls");
            LTNsubgoal1.done = true;

            LTNsubgoal2.Create(LTType.Subgoal, "Rings");
            LTNsubgoal2.requirements.Add(LTNsubgoal1);
            LTNsubgoal2.done = true;

            LTNsubgoal3.Create(LTType.Subgoal, "Clubs");
            LTNsubgoal3.requirements.Add(LTNsubgoal1);
            

            var lTNodes = FindObjectsOfType(typeof(LTNode)) as LTNode[];
            foreach(var startnode in lTNodes)
            {
                foreach (var endnode in startnode.requirements)
                {
                    print(startnode.title + "(" + startnode.transform.position.ToString() + ") ->" + endnode.title + "(" + endnode.transform.position.ToString() + ")");
                    var connection = Instantiate(prefabLTConnection, new Vector3(0, 0, 0), Quaternion.identity);
                    connection.GetComponent<LTConnection>().Create(startnode, endnode);
                }
            }
        }
        if (Input.GetKeyDown(KeyCode.Keypad1))
        {
            var lTNodes = FindObjectsOfType(typeof(LTNode)) as LTNode[];
            foreach (var node in lTNodes)
            {
                node.GetComponent<LTNodeVisualizer>().MaterialUpdate();
            }
        }

    }
}
