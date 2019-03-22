using System;
using UnityEngine;

/*
 * The main camera is used to calculate the positions of the touches. Therefore it is important to ensure that the camera of this scene is the main camera. 
 * Without the script the camera in the very first scene will be the main camera
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

    #region implementation of ICameraController
    public Camera[] FetchCameras() {
        return FindObjectsOfType<Camera>();
        
    }
    public Camera FetchMainCamera() {
        return Camera.main;
    }
    public Camera FetchAttachedCamera() {
        return (GetComponent<Camera>());
    }
    #endregion
}



// Logic that is not directly coupled with the game engine
// This is presumably what we want to actually test!
[Serializable]
public class MainCameraSwitcherLogic {
    // EVERYTHING to do with game engine through the controller
    private ICameraController cameraController;
    private Camera initialMainCamera;


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

    public void ActivateOnlyCamera(Camera camera) {
        Camera[] cameras = cameraController.FetchCameras();
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

// methods that use the game engine (or are mocked)
public interface ICameraController {
    Camera FetchMainCamera();
    Camera[] FetchCameras();
    Camera FetchAttachedCamera();
}
