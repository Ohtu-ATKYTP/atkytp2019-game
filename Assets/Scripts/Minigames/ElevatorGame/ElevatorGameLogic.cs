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
    private GameObject infoText;
    

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

        infoText = GameObject.FindGameObjectWithTag("InfoText");
        
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

    public void setDifficulty(){
        int difficulty = dataController.GetDifficulty();
        float reducedTime = (float) 25 - difficulty*3;
        if(reducedTime < 8){
            reducedTime = 8;
        }
        timer.SetTime(reducedTime);
    }

    public void WinMinigame() {
        foreach (GameObject jumper in jumpmanList){
            jumper.GetComponent<Button>().interactable = false;
            jumper.GetComponent<JumpmanLogic>().gameWon = true;
            jumper.GetComponent<JumpmanLogic>().ChangeToScared();
        }

        brokenBorder.SetActive(true);
        supportBorder.SetActive(false);

        GameObject.FindObjectOfType<ElevatorShaftMove>().endedGame = true;

        foreach(GameObject border in borders){
                border.GetComponent<BorderLogic>().AddRigidBody();
        }
        
        EndGame(true);
    }
    
    public void LoseMinigame() {
        
        infoText.SetActive(true);
        infoText.GetComponent<Text>().text = "TIME OVER"; 
        
        endedGame = true;
        foreach (GameObject jumper in jumpmanList){
            jumper.GetComponent<Button>().interactable = false;
        }
        EndGame(false);
    }

    public void OnTimerEnd() {
        this.LoseMinigame();
    }

    public async void EndGame(bool won) {
        timer.StopTimerProgression();
        await new WaitForSecondsRealtime(3);
        dataController.MinigameEnd(won, won ? 10 : 0);
    }
}
