using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElevatorGameLogic : MonoBehaviour {
    private GameObject[] Borders;
    private BorderLogic BorderLogic;
    private int Damage;

    private GameObject[] JumpmanList;
    void Start() {
        Damage = 0;
        Borders = GameObject.FindGameObjectsWithTag("Border");
        JumpmanList =  GameObject.FindGameObjectsWithTag("Jumpman");
    }
    public void AddDamage(){
        Damage += 1;
        if(Damage == 20){
            foreach(GameObject Border in Borders){
                Border.GetComponent<BorderLogic>().AddRigidBody();
            }
            this.ChangeFace();
        }
    }
    public void ChangeFace(){
        foreach (GameObject Jumper in JumpmanList){
            Jumper.GetComponent<JumpmanLogic>().ChangeFace();
        }
    }
}
