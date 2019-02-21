using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * The main camera is used to calculate the positions of the touches. Therefore it is important to ensure that the camera of this scene is the main camera. 
 * Without the script the camera in the very first scene will be the main camera
 * 
 * 
 * 
 */
public class MainCameraSwitcher : MonoBehaviour {
    private Camera initialMainCamera; 

    void Start() {
        initialMainCamera = Camera.main;
        activateOnlyCamera(GetComponent<Camera>());
    }


    private void OnDestroy() {
        activateOnlyCamera(initialMainCamera);
    }

        private void activateOnlyCamera(Camera camera) {
        Camera[] cameras = FindObjectsOfType<Camera>();
        for (int i = 0; i < cameras.Length; i++) {
            if (cameras[i] != camera) {
                cameras[i].enabled = false;
            } else {
                cameras[i].enabled = true;
            }
        }
    }
}
