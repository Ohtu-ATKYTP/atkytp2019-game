using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShakeBehavior : MonoBehaviour {
    
    private float shakeDuration = 0f;
    private float shakeMagnitude = 15f;
    private float dampingSpeed = 1.0f;
    Vector3 initialPosition;

    void Start() {
        this.initialPosition = transform.localPosition;
    }

    void Update() {
        
        if (shakeDuration > 0) {
            transform.localPosition = initialPosition + Random.insideUnitSphere * shakeMagnitude;
            shakeDuration -= Time.deltaTime * dampingSpeed;
        }
        else {
            shakeDuration = 0f;
            transform.localPosition = initialPosition;
        }
    }

    public void TriggerShake() {
        shakeDuration = 0.2f;
    }

}
