using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpmanLogic : MonoBehaviour {
    private bool onGround;
    void Start() {
        onGround = true;
    }
    void Update() {
        
    }
    void OnCollisionEnter(Collision collision){
        onGround = true;
        //doDamage()
    }

    public void Jump() {
        GetComponent<Rigidbody2D>().AddForce(Vector2.up*3000, ForceMode2D.Impulse);
    }


}
