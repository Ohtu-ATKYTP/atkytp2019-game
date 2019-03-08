using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionLogic : MonoBehaviour
{
    private ElevatorGameLogic EGLogic;
    private ShakeBehavior shake;
    
    void Start() {
        this.EGLogic = FindObjectOfType<ElevatorGameLogic>();
        this.shake = FindObjectOfType<ShakeBehavior>();
    }

    void OnCollisionEnter2D(Collision2D collision2D){
        EGLogic.AddDamage();
        shake.TriggerShake();

    }
}
