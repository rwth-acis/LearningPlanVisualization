using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LTConnection : MonoBehaviour
{
    public LTNode start;
    public LTNode end;
    private Vector3 startPosition;
    private Vector3 endPosition;

    // Start is called before the first frame update
    public void Create(LTNode newStart, LTNode newEnd)
    {
        start = newStart;
        end = newEnd;
    }


    void Update()
    {
        if (start.GetComponent<Visibility>().Visible && end.GetComponent<Visibility>().Visible)
        {
            startPosition = start.transform.position;
            endPosition = end.transform.position;

            transform.localScale = new Vector3(1, Vector3.Magnitude(endPosition - startPosition), 1);
            transform.up = endPosition - startPosition;
            transform.position = (endPosition + startPosition) * 0.5f;
        }
        else
        {
            transform.localScale = new Vector3(0, 0, 0);
        }
    }

}
