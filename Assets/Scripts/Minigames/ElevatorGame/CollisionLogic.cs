using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionLogic : MonoBehaviour
{
    private ElevatorGameLogic EGLogic;
    private ShakeBehavior shake;
    private EffectsController effects;
    private int collisionCount;

    private GameObject elevatorDoors;
    private GameObject[] borders;
    
    void Start() {
        this.EGLogic = FindObjectOfType<ElevatorGameLogic>();
        this.shake = FindObjectOfType<ShakeBehavior>();
        this.effects = FindObjectOfType<EffectsController>();
        this.collisionCount = 0;

        elevatorDoors = GameObject.Find("ElevatorDoors");
        borders = GameObject.FindGameObjectsWithTag("Border");
    }

    void OnCollisionEnter2D(Collision2D collision2D){
        if(collisionCount < 3){
            collisionCount += 1;
        }
        else{
            shake.TriggerShake();
        }

        if(EGLogic.forceDownActive==true){
            EGLogic.AddDamage(1f);
            effects.ShowStars();
            EGLogic.forceDownActive=false;
            this.AddDamageVisuals();
        }

    }

    private void AddDamageVisuals(){
        elevatorDoors.GetComponent<DamageVisuals>().DamageVisual();
        foreach(GameObject border in borders){
                border.GetComponent<DamageVisuals>().DamageVisual();
        }
    }
}
