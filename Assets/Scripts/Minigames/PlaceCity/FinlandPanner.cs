using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinlandPanner : MonoBehaviour {
    private Vector2 direction;
    private bool moves = false;

    void Start() {
        Initialize(new Vector2(1, 0));
    }

    public void Initialize(Vector2 direction) {
        this.direction = direction;
        moves = true; 
    }

    void Update() {
        if (!moves) {
            return;
        }
        this.transform.position += Time.deltaTime * Vector3.ClampMagnitude(direction, 1f);
    }
}
