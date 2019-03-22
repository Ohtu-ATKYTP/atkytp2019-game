using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchToggle : MonoBehaviour
{

    void Update()
    {
        if (Input.touchCount > 0)
        {
            GetComponent<TrailRenderer>().emitting = true;
        }    
        else
        {
            GetComponent<TrailRenderer>().emitting = false;
        }
    }
}
