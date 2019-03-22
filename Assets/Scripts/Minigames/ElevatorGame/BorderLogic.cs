using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BorderLogic : MonoBehaviour {

    public void AddRigidBody(){
        Rigidbody2D RB = this.gameObject.AddComponent<Rigidbody2D>();
        RB.mass = 5;
        RB.gravityScale = 100;
    }
}

