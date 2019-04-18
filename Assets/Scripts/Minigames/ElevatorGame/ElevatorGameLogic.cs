using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ElevatorGameLogic : MonoBehaviour, IMinigameEnder {
    
    private GameObject[] jumpmanList;
    private GameObject[] borders;
    
    private MinigameLogic miniGameLogic; 
    private TimeProgress timer;
    private GameObject brokenRope;
    private GameObject supportRope;
    private GameObject infoText;
    private GameObject instructions;
    
    public bool forceDownActive;
    private float damage;
    private bool endedGame;

    void Start() {
        borders = GameObject.FindGameObjectsWithTag("Border");

        endedGame = false;
        damage = 0;

        jumpmanList =  GameObject.FindGameObjectsWithTag("Jumpman");
        miniGameLogic = GameObject.FindObjectOfType<MinigameLogic>();
        timer = FindObjectOfType<TimeProgress>();
        
        brokenRope = GameObject.Find("BrokenSupportRopes");
        brokenRope.SetActive(false);

        supportRope = GameObject.Find("SupportRope");

        infoText = GameObject.Find("InfoText");
        
        instructions = GameObject.FindGameObjectWithTag("Instructions");
        instructions.SetActive(false);
        if(DataController.GetDifficulty() == 1){
            DisplayInstructions();
        }
        timer.SetTime(10);
    }

    public async void DisplayInstructions(){
        instructions.SetActive(true);
        await new WaitForSecondsRealtime(5);
        instructions.SetActive(false);
        timer.SetTime(10);
    }

    //Damage after smash. Right now win with 1, but option to add more.
    public void AddDamage(float DMG){
        damage += DMG;
        
        if(damage >= 1 && !endedGame){
            endedGame = true;
            this.WinMinigame();
        }
    }

    public void WinMinigame() {
        foreach (GameObject jumper in jumpmanList){
            jumper.GetComponent<Button>().interactable = false;
            jumper.GetComponent<JumpmanLogic>().ChangeToScared();
        }

        brokenRope.SetActive(true);
        supportRope.SetActive(false);

        GameObject.FindObjectOfType<ElevatorShaftMove>().move = false;

        this.AddRigidBodies();
        
        EndGame(true);
    }

    private void AddRigidBodies(){
        foreach(GameObject border in borders){
                border.GetComponent<AddRigidBody>().AddRB();
        }
        GameObject.Find("ElevatorDoors").GetComponent<AddRigidBody>().AddRB();
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
        GameManager.endMinigame(won, won ? 10 : 0);
    }

    public void PressForceDownButton(){
        forceDownActive = true;
        foreach (GameObject jumper in jumpmanList){
            jumper.GetComponent<JumpmanLogic>().ForceDown();
        }
    }
}
