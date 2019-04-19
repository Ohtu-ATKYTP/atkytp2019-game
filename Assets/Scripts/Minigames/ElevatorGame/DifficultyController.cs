using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DifficultyController : MonoBehaviour {

    private GameObject[] jumpmanList;
    
    void Start() {
        jumpmanList =  GameObject.FindGameObjectsWithTag("Jumpman");
        this.SetGravityScales();
    }
    
    private void SetGravityScales(){

        int difficulty = DataController.GetDifficulty();
        float gravScaleAdjuster = difficulty;

        foreach (GameObject jumper in jumpmanList){
            jumper.GetComponent<JumpmanLogic>().SetGravScale(gravScaleAdjuster);
        }
    }

}
