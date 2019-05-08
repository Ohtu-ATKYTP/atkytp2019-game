using UnityEngine;

public class AddRigidBody : MonoBehaviour {

    public void AddRB(){
        Rigidbody2D RB = GetComponent<Rigidbody2D>(); 
        if(RB == null) {
            RB = this.gameObject.AddComponent<Rigidbody2D>();
        }
        RB.mass = 5;
        RB.gravityScale = 100;
    }
}

