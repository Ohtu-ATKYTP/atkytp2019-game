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
    
    void Start() {
        damage = 0;
        borders = GameObject.FindGameObjectsWithTag("Border");
        jumpmanList =  GameObject.FindGameObjectsWithTag("Jumpman");
        miniGameLogic = GameObject.FindObjectOfType<MinigameLogic>();
        dataController = FindObjectOfType<DataController>();
        timer = FindObjectOfType<TimeProgress>();

        this.setDifficulty();
    }

    public void AddDamage(){
        damage += 1;
        if(damage == 20){
            foreach(GameObject border in borders){
                border.GetComponent<BorderLogic>().AddRigidBody();
            }
            this.ChangeFace();
            miniGameLogic.EndMinigame(true);
        }
    }

    public void ChangeFace(){
        foreach (GameObject jumper in jumpmanList){
            jumper.GetComponent<JumpmanLogic>().ChangeFace();
        }
    }

    public void setDifficulty(){
        //int difficulty = dataController.GetDifficulty();
        //int reducedTime = 25 - difficulty*5;
        //if(reducedTime < 10){
        //    reducedTime = 10;
        //}
        float time = 5f;
        Debug.Log("Difficulty time set to "+ time);
        timer.seconds = time;
        timer.setTime(time);
    }
}
