using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DifficultyAdjuster : MonoBehaviour {
    public TimeProgress timer; 
    public SpriteRenderer finlandSprite; 

    void Start() {
        timer = FindObjectOfType<TimeProgress>();
        finlandSprite = GameObject.Find("Finland").GetComponent<SpriteRenderer>();

    }

    void Update() {

    }
}
