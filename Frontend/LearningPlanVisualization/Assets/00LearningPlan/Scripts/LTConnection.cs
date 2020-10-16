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

    // Start is called before the first frame update
    public void Create(LTNode newStart, LTNode newEnd)
    {
        start = newStart;
        end = newEnd;
        startVisibility = start.GetComponent<Visibility>();
        endVisibility = end.GetComponent<Visibility>();
        renderer = GetComponentInChildren<Renderer>();
    }


    void Update()
    {
        if (startVisibility.Visible && endVisibility.Visible)
        {
            startPosition = start.transform.position;
            endPosition = end.transform.position;
            var length = Vector3.Magnitude(endPosition - startPosition);
            transform.localScale = new Vector3(1,length, 1);
            transform.up = endPosition - startPosition;
            transform.position = (endPosition + startPosition) * 0.5f;
            renderer.material.mainTextureScale = new Vector2(3f, length);

        }
        else
        {
            transform.localScale = new Vector3(0, 0, 0);
        }
    }

}
