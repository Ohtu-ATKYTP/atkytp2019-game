//Keeps track if all jumpers are high enough. If yes, then display forceDownButton to smash.

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumperPositions : MonoBehaviour {

    private GameObject[] jumpmanList;
    private int jumpmenHighEnough;
    private GameObject forceDownButton;

    void Start() {
        forceDownButton = GameObject.Find("ForceDownButton");
        forceDownButton.SetActive(false);
    }


    void Update() {
        this.checkJumpmenHighEnough();
    }

    public void increaseJumpmenHighEnough(){
        this.jumpmenHighEnough++;
    }

    public void decreaseJumpmenHighEnough(){
        this.jumpmenHighEnough--;
    }

    private void checkJumpmenHighEnough(){
        if (jumpmenHighEnough==3){
            this.DisplayForceDownButton(true);
        }else{
            this.DisplayForceDownButton(false);  
        }
    }

    public void DisplayForceDownButton(bool visible){
        forceDownButton.SetActive(visible);
    }
}
