using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrailManager : MonoBehaviour
{
    private TrailRenderer trail;

    void Start()
    {
        trail = GetComponent<TrailRenderer>();
    }

    void Update()
    {
        if(Input.touchCount > 0)
        {
            trail.emitting = true;
        }else
        {
            trail.emitting = false;
        }
    }
}
