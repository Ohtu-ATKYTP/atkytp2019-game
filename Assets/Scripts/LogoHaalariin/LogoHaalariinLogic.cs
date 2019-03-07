using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LogoHaalariinLogic : MonoBehaviour {
    private DataController dataController;
    private int haalari;
    private int logo;
    public int associationAmount;
    public HaalariUpdater haalariUpdater;
    public LogoUpdater logoUpdater;
    void Start () {
        this.haalari = Random.Range (0, associationAmount);
        haalariUpdater.changeImage (this.haalari);
        this.logo = Random.Range (0, associationAmount);
        logoUpdater.changeImage (this.logo);
        dataController = FindObjectOfType<DataController> ();
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
    public void endGame () {
        if (this.logo == this.haalari) {
            dataController.MinigameEnd (true, 10);
        } else {
            dataController.MinigameEnd (false, 0);
        }
    }
    public void timesUp () {
        dataController.MinigameEnd (false, 0);
    }
}