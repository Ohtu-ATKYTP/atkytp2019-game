using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElevatorShaftMove : MonoBehaviour
{
    public bool endedGame;
    private float speed;

    void Start() {
        endedGame = false;
        speed = 1f;
    }
    
    void Update() {
        if(!endedGame) {
            Vector2 position = transform.position;
            position.y -= speed;
            transform.position = position;
        }
    }
}
