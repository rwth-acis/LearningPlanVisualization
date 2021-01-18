using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LTConnection : MonoBehaviour
{
    public LTNode start;
    public LTNode end;
    public GameObject cylinder;
    private Vector3 startPosition;
    private Vector3 endPosition;
    public Renderer mainRenderer;
    private Visibility startVisibility;
    private Visibility endVisibility;
    private bool visible = false;
    private float length;
    private Vector3 storedLocalScale = Vector3.one;


    // Start is called before the first frame update
    public void Create(LTNode newStart, LTNode newEnd)
    {
        start = newStart;
        end = newEnd;
        startVisibility = start.GetComponent<Visibility>();
        endVisibility = end.GetComponent<Visibility>();

        start.OnChangePosition += HandleChangePosition;
        end.OnChangePosition += HandleChangePosition;
        HandleChangePosition();

        if (startVisibility.Visible && endVisibility.Visible)
        {
            visible = true;
            transform.localScale = storedLocalScale;
        }
        else
        {
            visible = false;
            storedLocalScale = transform.localScale;
            transform.localScale = Vector3.zero;
        }
    }

    void HandleChangePosition()
    {
        startPosition = start.transform.position;
        endPosition = end.transform.position;
        cylinder.transform.up = endPosition - startPosition;
        transform.position = (endPosition + startPosition) * 0.5f;
        length = Vector3.Magnitude(endPosition - startPosition);
        mainRenderer.material.mainTextureScale = new Vector2(2f, 6f * length);
        cylinder.transform.localScale = new Vector3(1, length,1);
    }

    void Update()
    {
        if (startVisibility.Visible && endVisibility.Visible)
        {
            if (!visible)
            {
                visible = true;
                transform.localScale = storedLocalScale;
            }
        }
        else
        {
            if (visible)
            {
                visible = false;
                storedLocalScale = transform.localScale;
                transform.localScale = Vector3.zero;
            }
        }
    }

    public void Delete()
    {
        start.requirements.Remove(end);
        start.GetComponentInParent<LTNodeVisualizer>().MaterialUpdate();
        Destroy(gameObject);
    }
    private void OnDestroy()
    {
        start.OnChangePosition -= HandleChangePosition;
        end.OnChangePosition -= HandleChangePosition;
    }
}
