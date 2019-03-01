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
    // Start is called before the first frame update
    public void StartGame()
    {
		//dataController.SetRoundEndStatus(true);
		//dataController.SetReadyStatus(true);
		dataController.SetStatus(DataController.Status.MINIGAME);
    }

}
