using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]

public class Draggable : MonoBehaviour {
    private Vector3 screenPoint;
    private Vector3 offset;
    private Rigidbody2D rbody;
    
    public float forceMultiplier = .6f;
    public float maxAccelerationIndex = 200000f;

    private void Start() {
        rbody = gameObject.GetComponent<Rigidbody2D>();
    }

    void OnMouseDown() {
        //offset means the offset between cursor position and clicked object's origin
        screenPoint = Camera.main.WorldToScreenPoint(gameObject.transform.position);
        offset = gameObject.transform.position - Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z));

    }

    void OnMouseDrag() {
        Vector3 curScreenPoint = new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z);
        Vector3 curPosition = Camera.main.ScreenToWorldPoint(curScreenPoint) + offset;

        //Notice that we implicitly convert to a 2 dimensional vector, because we don't use the depth (z) component
        Vector2 target = curPosition - gameObject.transform.position;

        //Below is script for cubic acceleration
        Vector2 direction = target.normalized;
        float distance = target.magnitude;

        Vector2 newForce = direction * Mathf.Clamp(Mathf.Pow(distance, 2),0.0f, maxAccelerationIndex) * forceMultiplier;

        //Below is script for linear acceleration
        /* 
        Vector2 newForce = target * forceMultiplier;
        */

        rbody.AddForce(newForce);
    }

    void Update() {
        if (rbody.velocity.y < 0) {
            rbody.AddForce(new Vector3(0.0f, -1000.0f, 0.0f));
        }
    }

}
