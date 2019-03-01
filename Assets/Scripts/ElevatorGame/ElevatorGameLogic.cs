using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElevatorGameLogic : MonoBehaviour {
    
    private int Damage;
    void Start() {
        Damage = 0;
    }
    void Update(){
        //if damage x then y
    }

    public void AddDamage(){
        Damage += 1;
    }
}
