using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LogoHaalariinLogic : MonoBehaviour {
    private DataController dataController;
    private TimeProgress timer;
    private int haalari = 0;
    private int logo = 0;
    private int difficulty = 1;
    public int associationAmount;
    public HaalariUpdater haalariUpdater;
    public LogoUpdater logoUpdater;

    void Start () {
        dataController = FindObjectOfType<DataController> ();
        timer = FindObjectOfType<TimeProgress> ();
        initializeGame ();
    }
    private void initializeGame () {
        if (dataController != null) {
            difficulty = dataController.GetDifficulty ();
        }
        //Setting initial haalari and logo
        this.haalari = Random.Range (0, associationAmount);
        haalariUpdater.changeImage (this.haalari);
        this.logo = Random.Range (0, associationAmount);
        logoUpdater.changeImage (this.logo);
        //Setting game time
        int time = 15 - this.difficulty;
        if (time < 3) {
            time = 3;
        }
        timer.SetTime (time);
    }
    public void nextLogo () {
        this.logo++;

        if (this.logo > associationAmount - 1) {
            this.logo = 0;
        }
        logoUpdater.changeImage (this.logo);
    }
    public void prevLogo () {
        this.logo--;
        if (this.logo < 0) {
            this.logo = associationAmount - 1;
        }
        logoUpdater.changeImage (this.logo);
    }
    public async void endGame () {
        timer.StopTimerProgression();
        if (this.logo == this.haalari) {
            logoUpdater.startRotateLogoAnimation();
            await new WaitForSecondsRealtime(3);
            dataController.MinigameEnd (true, 10);
        } else {
            logoUpdater.startDropLogoAnimation();
            await new WaitForSecondsRealtime(3);
            dataController.MinigameEnd (false, 0);
        }
    }
    public async void timesUp () {
        logoUpdater.startDropLogoAnimation();
        await new WaitForSecondsRealtime(3);
        dataController.MinigameEnd (false, 0);
    }
}