﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuManager : MonoBehaviour {
    [SerializeField] private Canvas registrationCanvas; 
    [SerializeField] private Canvas mainMenuCanvas; 
    [SerializeField] private Canvas highScoreCanvas; 
    [SerializeField] private Canvas settingsCanvas;

    Canvas[] canvases;

    private void Start() {
        canvases = new Canvas[4];
        canvases[0] = registrationCanvas; 
        canvases[1] = mainMenuCanvas; 
        canvases[2] = highScoreCanvas; 
        canvases[3] = settingsCanvas; 
    }



    public void displayOnlyMenu(string canvasName) {
        string nameOfCanvas = "ERROR"; 
        if(canvasName.Equals(registrationCanvas.name)){
            nameOfCanvas = "REGISTER";     
        } else if(canvasName.Equals(mainMenuCanvas.name)){
            nameOfCanvas = "MAIN MENU"; 
            } else if(canvasName.Equals(highScoreCanvas.name)){
               nameOfCanvas = "HIGH SCORE"; 
            } else if(canvasName.Equals(settingsCanvas.name)){
                nameOfCanvas = "SETTINGS"; 
            }

        Debug.Log("Will show menu " + nameOfCanvas);
        setOneCanvasActive(canvasName); 
            
    }

    private void setOneCanvasActive(string name){ 
            foreach( Canvas c in canvases){ 
                if(c.name.Equals(name)){
                    c.enabled = true; 
                } else {
                    c.enabled = false; 
                }
            }
        }

}
