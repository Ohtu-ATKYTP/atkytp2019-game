using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ElevatorGameLogic : MonoBehaviour, IMinigameEnder {
    private MinigameLogic miniGameLogic;
    private GameObject[] borders;
    private BorderLogic borderLogic;
    //private DataController dataController;
    private float damage;
    private TimeProgress timer;
    private GameObject[] jumpmanList;
    private GameObject brokenBorder;
    private GameObject supportBorder;
    private GameObject infoText;
    private GameObject elevatorDoors;

    private GameObject forceDownButton;

    private GameObject instructions;
    
    private int jumpmenHighEnough;

    private bool endedGame;

    private bool forceDownButtonCoolTime;
    
    void Start() {

        endedGame = false;
        forceDownButtonCoolTime = false;

        damage = 0;
        borders = GameObject.FindGameObjectsWithTag("Border");
        jumpmanList =  GameObject.FindGameObjectsWithTag("Jumpman");
        miniGameLogic = GameObject.FindObjectOfType<MinigameLogic>();
        //dataController = FindObjectOfType<DataController>();
        timer = FindObjectOfType<TimeProgress>();
        supportBorder = GameObject.FindGameObjectWithTag("SupportBorder");
        brokenBorder = GameObject.FindGameObjectWithTag("BrokenBorder");
        brokenBorder.SetActive(false);

        infoText = GameObject.FindGameObjectWithTag("InfoText");

        jumpmenHighEnough = 0;

        forceDownButton = GameObject.FindGameObjectWithTag("ForceDownButton");
        forceDownButton.SetActive(false);

        elevatorDoors = GameObject.Find("ElevatorDoors");
        
        //this.setDifficulty();

        instructions = GameObject.FindGameObjectWithTag("Instructions");
        instructions.SetActive(false);
        
        if(DataController.GetDifficulty() == 1){
            DisplayInstructions();
        }

        timer.SetTime(30);
    }

    void Update() {
        this.checkJumpmenHighEnough();
    }

    public async void DisplayInstructions(){
        instructions.SetActive(true);
        await new WaitForSecondsRealtime(5);
        instructions.SetActive(false);
        timer.SetTime(30);

    }

    public async void AddDamage(float DMG){
        //Debug.Log("Height based damage is: " + heightDMG);
        damage += DMG;
        ///Debug.Log("NEW DAMAGE IS: " + damage);
        supportBorder.GetComponent<SupportBorderScript>().DamageVisual(damage);
        elevatorDoors.GetComponent<SupportBorderScript>().DamageVisual(damage);
        foreach(GameObject border in borders){
                border.GetComponent<SupportBorderScript>().DamageVisual(damage);
        }
        if(damage >= 3 && !endedGame){
            endedGame = true;
            await new WaitForSecondsRealtime((float) 0.3);
            this.WinMinigame();
        }
    }

    public void setDifficulty(){
        //int difficulty = DataController.GetDifficulty();
        //float reducedTime = (float) 25 - difficulty*3;
        //if(reducedTime < 8){
        //    reducedTime = 8;
        //}
        //timer.SetTime(reducedTime);


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
        //this.LoseMinigame();
    }

    public async void EndGame(bool won) {
        timer.StopTimerProgression();
        await new WaitForSecondsRealtime(3);
        GameManager.endMinigame(won, won ? 10 : 0);
    }

    public void increaseJumpmenHighEnough(){
        this.jumpmenHighEnough++;
    }

    public void decreaseJumpmenHighEnough(){
        this.jumpmenHighEnough--;
    }

    private void checkJumpmenHighEnough(){
        if (jumpmenHighEnough==3 && forceDownButtonCoolTime == false){
            forceDownButton.SetActive(true);
            ScreenCapture.CaptureScreenshot("unitygamepic");
        }else{
            forceDownButton.SetActive(false);   
        }
    }

    public void PressForceDownButton(){
        this.AddDamage(1);

        foreach (GameObject jumper in jumpmanList){
            jumper.GetComponent<JumpmanLogic>().ForceDown();
            forceDownButton.SetActive(false);
            //forceDownButtonCoolTime = true;
            //await new WaitForSecondsRealtime((float) 0.5);
            //forceDownButtonCoolTime = false;
        }
    }
}
