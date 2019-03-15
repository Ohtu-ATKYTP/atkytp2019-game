using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class PlayButton : MonoBehaviour
{
    private DataController dataController;
    private void Start() {
        dataController = FindObjectOfType<DataController>();
    }

    public void StartGame() {
		dataController.SetDebugMode(false);
		dataController.SetStatus(DataController.Status.MINIGAME);
    }

}
