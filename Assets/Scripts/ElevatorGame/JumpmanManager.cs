using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpmanManager : MonoBehaviour{   
    
    private GameObject[] JumpmanList;
    private Rigidbody selfRigidbody;
    void Start() {
        JumpmanList =  GameObject.FindGameObjectsWithTag("Jumpman");
    }

    void Update() {
    }

    public void Jump(){
        while(true){

            foreach (GameObject Jumper in JumpmanList){
                Jumper.rigidbody.AddForce(0, forceConst, 0, ForceMode.Impulse);
                //selfRigidbody.AddForce(0, forceConst, 0, ForceMode.Impulse);
            }
        WaitForSecondsRealtime(10);
        }
    }
}
