using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Factory : MonoBehaviour
{
    public GameObject prefabLTGoal;
    public GameObject prefabLTSubgoal;
    public GameObject prefabLTAction;

    public Mesh newMesh;

    public GameObject prefabLTConnection;
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
            var LTNgoal = Instantiate(prefabLTGoal, new Vector3(0, 0, 5), Quaternion.identity).GetComponentInChildren<LTGoal>();
            LTNgoal.Create("Juggling");


            var LTNsubgoalClubs = Instantiate(prefabLTSubgoal, new Vector3(-1.5f, 0, 3.5f), Quaternion.identity).GetComponentInChildren<LTSubgoal>();
            LTNsubgoalClubs.Create("Clubs");
            LTNgoal.requirements.Add(LTNsubgoalClubs);
            
            var LTNsubgoalRings = Instantiate(prefabLTSubgoal, new Vector3(1.5f, 0, 3.5f), Quaternion.identity).GetComponentInChildren<LTSubgoal>();
            LTNsubgoalRings.Create("Rings");
            LTNgoal.requirements.Add(LTNsubgoalRings);

            var LTNsubgoalBalls = Instantiate(prefabLTSubgoal, new Vector3(0, 0, 2), Quaternion.identity).GetComponentInChildren<LTSubgoal>();
            LTNsubgoalBalls.Create("Balls");
            LTNsubgoalRings.requirements.Add(LTNsubgoalBalls);
            LTNsubgoalClubs.requirements.Add(LTNsubgoalBalls);


            var LTNball5 = Instantiate(prefabLTAction, new Vector3(0, 1, 2), Quaternion.identity).GetComponentInChildren<LTAction>();
            LTNball5.Create("5 Balls");
            LTNball5.done = true;
            LTNsubgoalBalls.requirements.Add(LTNball5);

            var LTNball3 = Instantiate(prefabLTAction, new Vector3(1, 2, 2), Quaternion.identity).GetComponentInChildren<LTAction>();
            LTNball3.Create("3 Balls");
            LTNball3.done = true;
            LTNball5.requirements.Add(LTNball3);

            var LTNball2 = Instantiate(prefabLTAction, new Vector3(-1, 2, 2), Quaternion.identity).GetComponentInChildren<LTAction>();
            LTNball2.Create("2 Balls");
            LTNball2.done = true;
            LTNball5.requirements.Add(LTNball2);

            var LTNball1 = Instantiate(prefabLTAction, new Vector3(0, 3, 2), Quaternion.identity).GetComponentInChildren<LTAction>();
            LTNball1.Create("1 Ball");
            LTNball1.done = true;
            LTNball2.requirements.Add(LTNball1);
            LTNball3.requirements.Add(LTNball1);


            var LTNring5 = Instantiate(prefabLTAction, new Vector3(1.5f, 1, 3.5f), Quaternion.identity).GetComponentInChildren<LTAction>();
            LTNring5.Create("5 Rings");
            LTNring5.done = true;
            LTNsubgoalRings.requirements.Add(LTNring5);

            var LTNring3 = Instantiate(prefabLTAction, new Vector3(2.5f, 2, 3.5f), Quaternion.identity).GetComponentInChildren<LTAction>();
            LTNring3.Create("3 Rings");
            LTNring3.done = true;
            LTNring5.requirements.Add(LTNring3);

            var LTNring2 = Instantiate(prefabLTAction, new Vector3(0.5f, 2, 3.5f), Quaternion.identity).GetComponentInChildren<LTAction>();
            LTNring2.Create("2 Rings");
            LTNring2.done = true;
            LTNring5.requirements.Add(LTNring2);

            var LTNring1 = Instantiate(prefabLTAction, new Vector3(1.5f, 3, 3.5f), Quaternion.identity).GetComponentInChildren<LTAction>();
            LTNring1.Create("1 Ring");
            LTNring1.done = true;
            LTNring2.requirements.Add(LTNring1);
            LTNring3.requirements.Add(LTNring1);


            var LTNclub3 = Instantiate(prefabLTAction, new Vector3(-0.5f, 1, 3.5f), Quaternion.identity).GetComponentInChildren<LTAction>();
            LTNclub3.Create("3 Clubs");
            LTNsubgoalClubs.requirements.Add(LTNclub3);

            var LTNclubBurn = Instantiate(prefabLTAction, new Vector3(-2.5f, 1, 3.5f), Quaternion.identity).GetComponentInChildren<LTAction>();
            LTNclubBurn.Create("Burning Clubs");
            LTNsubgoalClubs.requirements.Add(LTNclubBurn);

            var LTNclub2 = Instantiate(prefabLTAction, new Vector3(-0.5f, 2, 3.5f), Quaternion.identity).GetComponentInChildren<LTAction>();
            LTNclub2.Create("2 Clubs");
            LTNclub3.requirements.Add(LTNclub2);

            var LTNclub1 = Instantiate(prefabLTAction, new Vector3(-0.5f, 3, 3.5f), Quaternion.identity).GetComponentInChildren<LTAction>();
            LTNclub1.Create("1 Club");
            LTNclub1.done = true;
            LTNclub2.requirements.Add(LTNclub1);
            LTNclubBurn.requirements.Add(LTNclub1);



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
            var lTSubgoals = FindObjectsOfType(typeof(LTSubgoal)) as LTSubgoal[];
            foreach(var subgoal in lTSubgoals)
            {
                subgoal.UpdateActions();
            }

            var lTNodes = FindObjectsOfType(typeof(LTNode)) as LTNode[];
            foreach (var node in lTNodes)
            {
                node.GetComponent<LTNodeVisualizer>().MaterialUpdate();
            }
        }
        if (Input.GetKeyDown(KeyCode.Keypad2))
        {
            var goal = FindObjectOfType<LTGoal>();
            var mesh = goal.GetComponentInChildren<MeshFilter>();
            mesh.mesh = newMesh;
        }

    }
}
