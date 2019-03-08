using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShakeBehavior : MonoBehaviour {
    // Transform of the GameObject you want to shake
    private Transform transform;
 
     // Desired duration of the shake effect
    private float shakeDuration = 0f;
 
    // A measure of magnitude for the shake. Tweak based on your preference
    private float shakeMagnitude = 15f;
 
    // A measure of how quickly the shake effect should evaporate
    private float dampingSpeed = 1.0f;
 
    // The initial position of the GameObject
     Vector3 initialPosition;

    
    void Start() {
        this.transform = GetComponent<Transform>();
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
        Debug.Log("IN SHAKE METHOD");
        shakeDuration = 0.2f;
    }

}
