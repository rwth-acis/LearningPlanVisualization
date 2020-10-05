using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LTConnection : MonoBehaviour
{
    public Vector3 start;
    public Vector3 end;
    // Start is called before the first frame update
    void Start()
    {
        if (start == end)
        {
            this.enabled = false;
        }
        else
        {
            transform.localScale = new Vector3(1, Vector3.Magnitude(end - start), 1);
            transform.up = end - start;
            transform.position = (end + start)*0.5f;
        }

    }

}
