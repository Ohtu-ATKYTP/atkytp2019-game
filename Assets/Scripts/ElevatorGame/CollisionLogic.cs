using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionLogic : MonoBehaviour
{
    private ElevatorGameLogic EGLogic;
    
    void Start() {
        this.EGLogic = FindObjectOfType<ElevatorGameLogic>();
    }
    void OnCollisionEnter2D(Collision2D collision2D){
        EGLogic.AddDamage();
    }
}
