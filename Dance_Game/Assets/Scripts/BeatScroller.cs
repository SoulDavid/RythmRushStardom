using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeatScroller : MonoBehaviour
{
    public float beatTempo;

    public bool hasStarted;

    public GameObject objectToInstantiate;

    // Start is called before the first frame update
    void Start()
    {
        hasStarted = false;
        beatTempo = beatTempo / 60f;
    }

    // Update is called once per frame
    void Update()
    {
        if(!hasStarted)
        {
            //if(Input.anyKeyDown)
            //{
            //    hasStarted = true;
            //}
        }
        else
        {
            GameObject newObj = Instantiate(objectToInstantiate) as GameObject;
            newObj.transform.position = transform.position + Vector3.down * 1 * 0.6f;
        }
    }
}
