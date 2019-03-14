using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElevatorGameLogic : MonoBehaviour {
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
            this.EndGame();
        }
    }

    

    public void EndGame(){
        brokenBorder.SetActive(true);
        supportBorder.SetActive(false);
        foreach(GameObject border in borders){
                border.GetComponent<BorderLogic>().AddRigidBody();
        }
        this.ChangeFace();
        miniGameLogic.EndMinigame(true);
    }

    public void ChangeFace(){
        foreach (GameObject jumper in jumpmanList){
            jumper.GetComponent<JumpmanLogic>().ChangeFace();
        }
    }

    public void setDifficulty(){
        int difficulty = dataController.GetDifficulty();
        float reducedTime = (float) 30 - difficulty*3;
        if(reducedTime < 10){
            reducedTime = 10;
        }
        
        Debug.Log("Difficulty time set to "+ reducedTime);
        timer.SetTime(reducedTime);

        //Vaikeusidea: tihennä hyppynopeutta (gravity + force)
    }

    
}
