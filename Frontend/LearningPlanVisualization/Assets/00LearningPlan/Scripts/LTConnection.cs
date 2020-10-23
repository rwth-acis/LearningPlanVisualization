using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LTConnection : MonoBehaviour
{
    public LTNode start;
    public LTNode end;
    private Vector3 startPosition;
    private Vector3 endPosition;
    private Renderer renderer;
    private Visibility startVisibility;
    private Visibility endVisibility;
    private bool visible = false;
    private float length;
    // Start is called before the first frame update
    public void Create(LTNode newStart, LTNode newEnd)
    {
        start = newStart;
        end = newEnd;
        startVisibility = start.GetComponent<Visibility>();
        endVisibility = end.GetComponent<Visibility>();
        renderer = GetComponentInChildren<Renderer>();

        start.OnChangePosition += HandleChangePosition;
        end.OnChangePosition += HandleChangePosition;
        HandleChangePosition();
    }

    void HandleChangePosition()
    {
        startPosition = start.transform.position;
        endPosition = end.transform.position;
        transform.up = endPosition - startPosition;
        transform.position = (endPosition + startPosition) * 0.5f;
        length = Vector3.Magnitude(endPosition - startPosition);
        renderer.material.mainTextureScale = new Vector2(3f, length);


        if (visible) transform.localScale = new Vector3(1, length, 1);
        else transform.localScale = Vector3.zero;
        
    }

    void Update()
    {
        if (startVisibility.Visible && endVisibility.Visible)
        {
            if (!visible)
            {
                visible = true;
                transform.localScale = new Vector3(1, length, 1);
            }
        }
        else
        {
            if (visible)
            {
                visible = false;
                transform.localScale = Vector3.zero;
            }
        }
    }

}
