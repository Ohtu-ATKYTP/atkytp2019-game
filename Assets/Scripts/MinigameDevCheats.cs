using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading.Tasks;

public class MinigameDevCheats : MonoBehaviour {
    private bool inMinigame;
    private TimeProgress timer;

    void Start() {
        inMinigame = false;
    }



    public void ConfigureForNonMinigame() {
        inMinigame = false;
    }


    // should be called when a minigame has already been loaded
    public async void ConfigureForNewMinigame() {
        inMinigame = true;
        while (!timer) {
            timer = FindObjectOfType<TimeProgress>();
            await Task.Delay(33);
        }

    }

    void Update() {
        if (!inMinigame) {
            return;
        }

        if (Input.GetKeyDown(KeyCode.Space)) {
            timer.TogglePause();
        } else if (Input.GetKeyDown(KeyCode.W)) {
            // win the minigame
        } else if (Input.GetKeyDown(KeyCode.L)) {
            // lose the minigame
        }
    }

}
