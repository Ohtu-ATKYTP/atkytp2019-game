using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ElevatorGameLogic : MonoBehaviour, IMinigameEnder {
    private MinigameLogic miniGameLogic;
    private GameObject[] borders;
    private BorderLogic borderLogic;
    private DataController dataController;
    private int damage;
    private TimeProgress timer;
    private GameObject[] jumpmanList;
    private GameObject brokenBorder;
    private GameObject supportBorder;

    private bool endedGame = false;
    
    void Start() {
        damage = 0;
        borders = GameObject.FindGameObjectsWithTag("Border");
        jumpmanList =  GameObject.FindGameObjectsWithTag("Jumpman");
        miniGameLogic = GameObject.FindObjectOfType<MinigameLogic>();
        dataController = FindObjectOfType<DataController>();
        timer = FindObjectOfType<TimeProgress>();
        supportBorder = GameObject.FindGameObjectWithTag("SupportBorder");
        brokenBorder = GameObject.FindGameObjectWithTag("BrokenBorder");
        brokenBorder.SetActive(false);
        
        this.setDifficulty();
    }

    public void AddDamage(){
        damage += 1;
        supportBorder.GetComponent<SupportBorderScript>().DamageVisual(damage);
        if(damage >= 20 && !endedGame){
            endedGame = true;
            this.WinMinigame();
        }
    }

    public void ChangeFace(){
        foreach (GameObject jumper in jumpmanList){
            jumper.GetComponent<JumpmanLogic>().ChangeFace();
        }
    }

    public void setDifficulty(){
        int difficulty = dataController.GetDifficulty();
        float reducedTime = (float) 25 - difficulty*3;
        if(reducedTime < 13){
            reducedTime = 13;
        }
        
        Debug.Log("Difficulty time set to "+ reducedTime);
        timer.SetTime(reducedTime);

        //Vaikeusidea: tihennä hyppynopeutta (gravity + force) tai ota randomius myöhemmin käyttöön?
    }

    public void WinMinigame() {
        foreach (GameObject jumper in jumpmanList){
            jumper.GetComponent<Button>().interactable = false;
        }
        brokenBorder.SetActive(true);
        supportBorder.SetActive(false);

        foreach(GameObject border in borders){
                border.GetComponent<BorderLogic>().AddRigidBody();
        }

        this.ChangeFace();
        miniGameLogic.EndMinigame(true);
    }
    
    public void LoseMinigame() {
        
        Text timeOutText = GameObject.FindGameObjectWithTag("InfoText").GetComponent<Text>();
        timeOutText.text = "TIME OVER";
        
        endedGame = true;
        foreach (GameObject jumper in jumpmanList){
            jumper.GetComponent<Button>().interactable = false;
        }
        miniGameLogic.EndMinigame(false);
    }

    public void OnTimerEnd() {
        this.LoseMinigame();
    }

    
}
