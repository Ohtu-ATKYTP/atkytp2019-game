using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElevatorGameLogic : MonoBehaviour {
    private GameObject[] Borders;
    private BorderLogic BorderLogic;
    private int Damage;
    void Start() {
        Damage = 0;
        Borders = GameObject.FindGameObjectsWithTag("Border");
    }
    void Update(){
    }
    public void AddDamage(){
        Damage += 1;
        if(Damage == 20){
            foreach(GameObject Border in Borders){
                Border.GetComponent<BorderLogic>().AddRigidBody();
            }
        }
    }
}
