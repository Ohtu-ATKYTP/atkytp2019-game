using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading.Tasks;
using System.Linq;

public class MinigameDevCheats : MonoBehaviour {
    private bool inMinigame;
    private TimeProgress timer;
    private IMinigameEnder minigameManager;

    void Start() {
        inMinigame = false;
    }



    public void ConfigureForNonMinigame() {
        inMinigame = false;
        minigameManager = null;
    }


    public async void ConfigureForNewMinigame() {
        inMinigame = true;
        while (!timer || (minigameManager == null)) {

            await Task.Delay(33);
            var x = FindObjectsOfType<MonoBehaviour>().OfType<IMinigameEnder>();
            minigameManager = x.First<IMinigameEnder>();
            timer = FindObjectOfType<TimeProgress>();

        }

    }

    void Update() {
        if (!inMinigame) {
            return;
        }

        if (Input.GetKeyDown(KeyCode.Space)) {
            timer.TogglePause();
        } else if (Input.GetKeyDown(KeyCode.W)) {
            minigameManager.WinMinigame();
        } else if (Input.GetKeyDown(KeyCode.L)) {
            minigameManager.LoseMinigame();
        }
    }

}
