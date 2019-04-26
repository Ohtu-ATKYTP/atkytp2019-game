using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LogoHaalariinLogic : MonoBehaviour {
    private TimeProgress timer;
    private int haalari = 0;
    private int logo = 0;
    public int associationAmount;
    public HaalariUpdater haalariUpdater;
    public LogoUpdater logoUpdater;

    void Start() {
        timer = FindObjectOfType<TimeProgress>();
        initializeGame();
    }
    private void initializeGame() {
        //Setting initial haalari and logo
        this.haalari = Random.Range(0, associationAmount);
        haalariUpdater.changeImage(this.haalari);
        this.logo = Random.Range(0, associationAmount);
        logoUpdater.changeImage(this.logo);
        
        int time = 12 - DataController.GetDifficulty();
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

    // win / lose in other cases except time running out 
    public async void endGame() {
        timer.StopTimerProgression();

        bool win = this.logo == this.haalari;

        if (win) {
            logoUpdater.startRotateLogoAnimation();
        } else {
            logoUpdater.startDropLogoAnimation();
        }

        await new WaitForSecondsRealtime(3);


        GameManager.endMinigame(win);
    }

    public async void timesUp() {
        logoUpdater.startDropLogoAnimation();
        await new WaitForSecondsRealtime(3);
        GameManager.endMinigame(false);
    }
}