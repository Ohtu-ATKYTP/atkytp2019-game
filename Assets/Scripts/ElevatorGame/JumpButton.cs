using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpButton : MonoBehaviour{
    private GameObject[] JumpmanList;

    void Start() {
        JumpmanList =  GameObject.FindGameObjectsWithTag("Jumpman");
    }
    public void Jump(){
        foreach (GameObject Jumper in JumpmanList){
            Jumper.GetComponent<Rigidbody2D>().AddForce(Vector2.up*3000, ForceMode2D.Impulse);
        }
    }
}
