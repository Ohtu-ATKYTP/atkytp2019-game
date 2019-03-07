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
            // easy: end the game, unload immediately
            // enjoyable: win the minigame, allow it to perform ending actions before unloading
        } else if (Input.GetKeyDown(KeyCode.L)) {
            // lose the minigame
            // see the comments with winning
        }
    }

}
