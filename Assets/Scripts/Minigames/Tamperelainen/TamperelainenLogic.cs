using UnityEngine;
using System;

public class TamperelainenLogic: MonoBehaviour, IMinigameEnder {
    private TimeProgress timer;
    private int difficulty = 1;


    void Start() {
        timer = FindObjectOfType<TimeProgress>();
        InitializeGame();
    }
    private void InitializeGame() {

        difficulty = DataController.GetDifficulty();

        //Setting game time
        int time = 4 + Math.Min(3, difficulty);

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
        GameManager.EndMinigame(win);
    }

    public void EndGame() {
        if (false) {
            EndGame(true);
        } else {
            EndGame(false);
        }
    }
    public async void TimesUp() {
        Time.timeScale = 0;
        EndGame(true);
    }
}