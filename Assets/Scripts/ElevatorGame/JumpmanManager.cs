using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpmanManager : MonoBehaviour{   
    
    private GameObject[] JumpmanList;
    public int forceConst = 500;
    void Start() {
        JumpmanList =  GameObject.FindGameObjectsWithTag("Jumpman");
    }

    void Update() {
    }

    public void Jump(){
        foreach (GameObject Jumper in JumpmanList){
            Jumper.GetComponent<Rigidbody2D>().AddForce(Vector2.up*1000, ForceMode2D.Impulse);
        }
    }
}
