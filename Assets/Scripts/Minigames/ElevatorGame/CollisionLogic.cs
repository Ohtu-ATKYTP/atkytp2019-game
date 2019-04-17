using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionLogic : MonoBehaviour
{
    private ElevatorGameLogic EGLogic;
    private ShakeBehavior shake;
    private EffectsController effects;
    private int collisionCount;
    
    void Start() {
        this.EGLogic = FindObjectOfType<ElevatorGameLogic>();
        this.shake = FindObjectOfType<ShakeBehavior>();
        this.effects = FindObjectOfType<EffectsController>();
        this.collisionCount = 0;
    }

    void OnCollisionEnter2D(Collision2D collision2D){
        if(collisionCount < 3){
            collisionCount += 1;
        }
        else{
            //EGLogic.AddDamage();
            effects.ShowStars();
            shake.TriggerShake();
        }

        if(EGLogic.forceDownActive==true){
            EGLogic.AddDamage(1f);
            effects.ShowStars();
            EGLogic.forceDownActive=false;
        }

    }
}
