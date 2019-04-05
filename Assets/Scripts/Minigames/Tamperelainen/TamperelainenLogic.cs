using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TamperelainenLogic: MonoBehaviour, IMinigameEnder {
    private DataController dataController;
    private TimeProgress timer;
    private int difficulty = 1;


    void Start() {
        dataController = FindObjectOfType<DataController>();
        timer = FindObjectOfType<TimeProgress>();
        initializeGame();
    }
    private void initializeGame() {
        if (dataController != null) {
            difficulty = dataController.GetDifficulty();
        }

        //Setting game time
        int time = 5 + this.difficulty;

        timer.SetTime(time);
    }



    public void WinMinigame() {
        EndGame(true);
    }

    public void LoseMinigame() {
        EndGame(false);
    }

    // win / lose in other cases except time running out 
    public async void EndGame(bool win) {
        timer.StopTimerProgression();
        await new WaitForSecondsRealtime(3);
        Time.timeScale = 1;
        dataController.MinigameEnd(win, win ? 10 : 0);
    }

    public void endGame() {
        if (false) {
            EndGame(true);
        } else {
            EndGame(false);
        }
    }
    public async void timesUp() {
        Debug.Log("TimesUp()");
        Time.timeScale = 0;
        EndGame(true);
    }
}