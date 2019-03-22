using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionLogic : MonoBehaviour
{
    private ElevatorGameLogic EGLogic;
    private ShakeBehavior shake;
    private int collisionCount;
    
    void Start() {
        this.EGLogic = FindObjectOfType<ElevatorGameLogic>();
        this.shake = FindObjectOfType<ShakeBehavior>();
        this.collisionCount = 0;
    }

    void OnCollisionEnter2D(Collision2D collision2D){
        if(collisionCount < 3){
            collisionCount += 1;
        }
        else{
            EGLogic.AddDamage();
            shake.TriggerShake();
        }

    }
}
