using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraSetupper : MonoBehaviour
{
    private Camera priorCamera;

    private void Awake()
    {
        if(FindObjectsOfType<Camera>().Length > GetComponentsInChildren<Camera>().Length)
        {
            priorCamera = Camera.main;
            priorCamera.enabled = false;
        }
   

        Camera[] cameras = GetComponentsInChildren<Camera>();
        for(int i = 0; i < cameras.Length; i++)
        {
            cameras[i].enabled = true;
        }

    }

    private void OnDestroy()
    {
        if(priorCamera != null)
        {
            priorCamera.enabled = true;
        }
    }


}
