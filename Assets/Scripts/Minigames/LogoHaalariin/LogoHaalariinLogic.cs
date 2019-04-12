using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LogoHaalariinLogic : MonoBehaviour, IMinigameEnder {
    private TimeProgress timer;
    private int haalari = 0;
    private int logo = 0;
    private int difficulty = 1;
    public int associationAmount;
    public HaalariUpdater haalariUpdater;
    public LogoUpdater logoUpdater;

    void Start() {
        timer = FindObjectOfType<TimeProgress>();
        initializeGame();
    }
    private void initializeGame() {
        difficulty = DataController.GetDifficulty();
        //Setting initial haalari and logo
        this.haalari = Random.Range(0, associationAmount);
        haalariUpdater.changeImage(this.haalari);
        this.logo = Random.Range(0, associationAmount);
        logoUpdater.changeImage(this.logo);
        //Setting game time
        int time = 15 - this.difficulty;
        if (time < 3) {
            time = 3;
        }
        timer.SetTime(time);
    }

    public void nextLogo() {
        this.logo++;

        if (this.logo > associationAmount - 1) {
            this.logo = 0;
        }
        logoUpdater.changeImage(this.logo);
    }
    public void prevLogo() {
        this.logo--;
        if (this.logo < 0) {
            this.logo = associationAmount - 1;
        }
        logoUpdater.changeImage(this.logo);
    }

    public void WinMinigame() {
        this.logo = this.haalari;
        logoUpdater.changeImage(logo);
        EndGame(true);
    }

    public void LoseMinigame() {
        EndGame(false);
    }

    // win / lose in other cases except time running out 
    public async void EndGame(bool win) {
        timer.StopTimerProgression();
        if (win) {
            logoUpdater.startRotateLogoAnimation();
        } else {
            logoUpdater.startDropLogoAnimation();
        }
        await new WaitForSecondsRealtime(3);
        GameManager.endMinigame(win, win ? 10 : 0);
    }

    public void endGame() {
        if (this.logo == this.haalari) {
            EndGame(true);
        } else {
            EndGame(false);
        }
    }
    public async void timesUp() {
        logoUpdater.startDropLogoAnimation();
        await new WaitForSecondsRealtime(3);
        GameManager.endMinigame(false, 0);
    }
}