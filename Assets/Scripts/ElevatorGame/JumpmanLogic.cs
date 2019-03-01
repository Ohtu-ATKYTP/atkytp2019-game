using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpmanLogic : MonoBehaviour {
    private bool onGround;
    private ElevatorGameLogic EGLogic;
    void Start() {
        this.EGLogic = FindObjectOfType<ElevatorGameLogic>();
        onGround = true;
    }
    void Update() {
    }
    void OnCollisionEnter2D(Collision2D collision2D){
        onGround = true;
        EGLogic.AddDamage();
    }

    public void Jump() {
        onGround = false;
        GetComponent<Rigidbody2D>().AddForce(Vector2.up*3000, ForceMode2D.Impulse);
    }


}