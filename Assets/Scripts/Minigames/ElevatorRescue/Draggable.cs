using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]

public class Draggable : MonoBehaviour {
    private Vector3 screenPoint;
    private Vector3 offset;
    private Rigidbody2D rbody;
    
    public float magnetMultiplier = .6f;
    private readonly float cursorDeltaMultiplier = 3000.0f;
    public float maxAccelerationIndex = 400000f;
    private Vector3 prevCursorPosition;

    private void Start() {
        rbody = gameObject.GetComponent<Rigidbody2D>();
    }

    void OnMouseDown() {
        screenPoint = Camera.main.WorldToScreenPoint(gameObject.transform.position);
        offset = gameObject.transform.position - Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z));
        prevCursorPosition = Input.mousePosition;

    }

    void OnMouseDrag() {
        Vector3 curScreenPoint = new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z);
        Vector3 curPosition = Camera.main.ScreenToWorldPoint(curScreenPoint) + offset;
        Vector2 deltaCursor = Input.mousePosition - prevCursorPosition;

        //Notice that we implicitly convert to a 2 dimensional vector, because we don't use the depth (z) component
        Vector2 target = curPosition - gameObject.transform.position;

        //Below is script for cubic acceleration
        Vector2 direction = target.normalized;
        float distance = target.magnitude;

        Vector2 newForce = direction * Mathf.Clamp(Mathf.Pow(distance, 2),0.0f, maxAccelerationIndex) * magnetMultiplier;

        //Below is script for linear acceleration
        /* 
        Vector2 newForce = target * magnetMultiplier;
        */

        newForce += deltaCursor * cursorDeltaMultiplier;
        rbody.AddForce(newForce);

        prevCursorPosition = Input.mousePosition;
    }

    void Update() {
        if (rbody.velocity.y < 0) {
            rbody.AddForce(new Vector3(0.0f, -1000.0f, 0.0f));
        }
    }

}
