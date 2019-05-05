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
    private bool gameOver = false;

    void Start() {
        timer = FindObjectOfType<TimeProgress>();
        InitializeGame();
    }
    private void InitializeGame() {
        //Setting initial haalari and logo
        this.haalari = Random.Range(0, associationAmount);
        haalariUpdater.ChangeImage(this.haalari);
        this.logo = Random.Range(0, associationAmount);
        logoUpdater.ChangeImage(this.logo);
        
        int time = 12 - DataController.GetDifficulty();
        if (time < 3) {
            time = 3;
        }

        timer.SetTime(time);
    }

    public void NextLogo() {
        this.logo++;

        if (this.logo > associationAmount - 1) {
            this.logo = 0;
        }
        logoUpdater.ChangeImage(this.logo);
    }
    public void PrevLogo() {
        this.logo--;
        if (this.logo < 0) {
            this.logo = associationAmount - 1;
        }
        logoUpdater.ChangeImage(this.logo);
    }

    // win / lose in other cases except time running out 
    public async void EndGame() {
        if (this.gameOver) {
            return;
        }
        this.gameOver = true;
        timer.StopTimerProgression();

        bool win = this.logo == this.haalari;

        if (win) {
            logoUpdater.startRotateLogoAnimation();
        } else {
            logoUpdater.StartDropLogoAnimation();
        }

        await new WaitForSecondsRealtime(3);

        GameManager.EndMinigame(win);
    }

    public async void TimesUp() {
        this.gameOver = true;
        logoUpdater.StartDropLogoAnimation();
        await new WaitForSecondsRealtime(3);
        GameManager.EndMinigame(false);
    }
}