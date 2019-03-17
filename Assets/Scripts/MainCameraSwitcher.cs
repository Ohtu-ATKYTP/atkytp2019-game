using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

/*
 * The main camera is used to calculate the positions of the touches. Therefore it is important to ensure that the camera of this scene is the main camera. 
 * Without the script the camera in the very first scene will be the main camera
 * 
 */
public class MainCameraSwitcher : MonoBehaviour, ICameraController {
    private MainCameraSwitcherLogic logic;

    // Callback methods that the game engine will call at appropriate times
    // Must be here, since they are called  on every MonoBehaviour attached to game objects
    private void OnEnable() {
        logic = new MainCameraSwitcherLogic();
        // this class is an implementation of ICameraController, so it can be set in the following way
        logic.SetCameraController(this);
        logic.Initialize();
    }

    private void OnDestroy() {
        logic.ResetMainCamera();
    }

    // implementations of interface methods
    public ICamera[] FetchCameras() {
        Camera[] cameras = FindObjectsOfType<Camera>();
        return cameras.Select(cam => 
        WrapperPool<Camera, CameraWrapper>.WrapComponent(cam)).ToArray();
    }
    public ICamera FetchMainCamera() {
        return WrapperPool<Camera, CameraWrapper>.WrapComponent(Camera.main);
    }
    public ICamera FetchAttachedCamera() {
        return WrapperPool<Camera, CameraWrapper>.WrapComponent(GetComponent<Camera>());
    }
}


public class CameraWrapper : ICamera, ISettableComponent<Camera> {
    public Camera camera;
    public bool enabled { get => camera.enabled; set => camera.enabled = value; }

    public CameraWrapper() { }

    public void SetComponent(Camera c) {
        this.camera = c;
    }
}

public interface ICamera {
    bool enabled { get; set; }
}


// methods that must use the game engine
public interface ICameraController {
    ICamera FetchMainCamera();
    ICamera[] FetchCameras();
    ICamera FetchAttachedCamera();
}

// Logic that is not directly coupled with the game engine
// This is presumably what we want to actually test!
[Serializable]
public class MainCameraSwitcherLogic {
    private ICameraController cameraController;
    private ICamera initialMainCamera;


    public void SetCameraController(ICameraController ctrl) {
        this.cameraController = ctrl;

    }

    public void Initialize() {
        initialMainCamera = cameraController.FetchMainCamera();
        ActivateOnlyCamera(cameraController.FetchAttachedCamera());
    }


    public void ResetMainCamera() {
        ActivateOnlyCamera(initialMainCamera);
    }

    public void ActivateOnlyCamera(ICamera camera) {
        ICamera[] cameras = cameraController.FetchCameras();
        for (int i = 0; i < cameras.Length; i++) {
            if (cameras[i] == camera) {
                if (cameras[i] != null) {
                    cameras[i].enabled = true;
                }
            } else {
                cameras[i].enabled = false;
            }
        }
    }
}
