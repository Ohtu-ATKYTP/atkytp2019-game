using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CameraMotionController : MonoBehaviour {
    private bool xAxis = false;
    private bool yAxis = false;
    public bool motionIsUsed = false;
    private GameObject controlledCam;
    private Rigidbody2D cameraBody;
    public GameObject centerPointPrefab;
    private System.Action rotationMethod;
    private float? prev;

    public void Initialize(Dictionary<string, Vector2> rotationalInfo) {
        Input.gyro.enabled = true;
        controlledCam = this.gameObject;
        cameraBody = GetComponent<Camera>().GetComponent<Rigidbody2D>();

        foreach (string movementType in rotationalInfo.Keys) {
            if (movementType == "rotation") {
                AllowMovementByRotating(rotationalInfo[movementType]);
            } else if (movementType == "panning") {
                AllowMovementByPanning(rotationalInfo[movementType]);
            }
        }
    }

    public void AllowMovementByRotating(Vector2 centerPoint) {
        motionIsUsed = true;
        if (centerPoint == Vector2.zero) {
            rotationMethod = RotateInPlace;
            cameraBody.constraints = RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezePositionY;
        } else {
            Instantiate(centerPointPrefab, centerPoint, Quaternion.identity);
            centerPointPrefab.GetComponent<FixedJoint2D>().connectedBody = this.GetComponent<Rigidbody2D>();
            rotationMethod = RotateAroundPoint;
        }

    }

    public void AllowMovementByPanning(Vector2 axis) {
        motionIsUsed = true;
        this.gameObject.AddComponent<SliderJoint2D>();
        SliderJoint2D slider = GetComponent<SliderJoint2D>();
        slider.anchor = axis;
    }

    // Update is called once per frame
    void Update() {
        if (!motionIsUsed) {
            return;
        }
        rotationMethod();

        //Debug.Log(filtered);
    }


    private void RotateAroundPoint() {

        Vector3 acceleration = Input.acceleration;

        acceleration.x = Mathf.Abs(acceleration.x) < .1f ? 0 : acceleration.x;
        acceleration.y = Mathf.Abs(acceleration.y) < .1f ? 0 : acceleration.y;
        acceleration.z = 0;
        cameraBody.AddForce(100 * acceleration);
    }

    private void RotateInPlace() {
        Vector3 acceleration = Input.acceleration;

        acceleration.x = Mathf.Abs(acceleration.x) < .001f ? 0 : acceleration.x;
        acceleration.y = Mathf.Abs(acceleration.y) < .001f ? 0 : acceleration.y;
        acceleration.z = 0;

        if (Mathf.Abs(acceleration.x) > 0 && Mathf.Abs(acceleration.x) < .3f) {
            acceleration.x *= 20;
        }
        //if (Mathf.Abs(acceleration.y) > 0 && Mathf.Abs(acceleration.y) < .3f) {
           // acceleration.y *= 20; 
        //}
        //cameraBody.AddForce(100 * acceleration);
        cameraBody.AddTorque((acceleration.x < 0 ? -1 : 1) * Vector3.SqrMagnitude(acceleration));
    }
}
