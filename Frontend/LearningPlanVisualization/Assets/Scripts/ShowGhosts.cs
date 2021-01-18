using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowGhosts : MonoBehaviour
{
    public GameObject ghostNode;
    private LTNode selfNode;
    // Start is called before the first frame update
    void Start()
    {
        selfNode = GetComponent<LTNode>();
    }

    public void BtnClicked()
    {
        LTMainMenu.instance.InvokeDestroyGhosts();
        if (selfNode.GetType() == typeof(LTAction))
        {
            var ghostAction = Instantiate(ghostNode).GetComponent<LTGhostConnection>();
            var offset = new Vector3((selfNode.requirements.Count + 1) * LTMainMenu.instance.repositionMargin, LTMainMenu.instance.repositionMargin, 0);
            ghostAction.Create(selfNode, offset, LTType.Action);
        }
        else if (selfNode.GetType() == typeof(LTSubgoal))
        {
            var requiredActions = 0;
            var requiredSubgoals = 0;
            foreach (var requirement in selfNode.requirements)
            {
                if (requirement.GetType() == typeof(LTAction)) requiredActions++;
                if (requirement.GetType() == typeof(LTSubgoal)) requiredSubgoals++;
            }
            var ghostAction = Instantiate(ghostNode).GetComponent<LTGhostConnection>();
            var offset = new Vector3((requiredActions + 1) * LTMainMenu.instance.repositionMargin, LTMainMenu.instance.repositionMargin, 0);
            ghostAction.Create(selfNode, offset, LTType.Action);

            var ghostSubgoal = Instantiate(ghostNode).GetComponent<LTGhostConnection>();
            offset = new Vector3((requiredSubgoals + 1) * LTMainMenu.instance.repositionMargin, 0, -LTMainMenu.instance.repositionMargin);
            ghostSubgoal.Create(selfNode, offset, LTType.Subgoal);
        }
        else if (selfNode.GetType()==typeof(LTGoal))
        {
            var ghostGoal = Instantiate(ghostNode).GetComponent<LTGhostConnection>();
            var offset = new Vector3((selfNode.requirements.Count + 1) * LTMainMenu.instance.repositionMargin, 0, -LTMainMenu.instance.repositionMargin);
            ghostGoal.Create(selfNode, offset, LTType.Subgoal);
        }
    }
}
