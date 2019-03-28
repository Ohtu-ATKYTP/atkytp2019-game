using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CameraMotionController : MonoBehaviour {
    private bool xAxis = false;
    private bool yAxis = false;
    private bool zAxis = false;
    public bool motionIsUsed = false;
    private Rigidbody2D controlledCam;

    private void Start() {
        if (motionIsUsed) {
            Initialize(true, false, false);
        }
    }

    void Initialize(bool x, bool y, bool z) {
        this.xAxis = x;
        this.yAxis = y;
        this.zAxis = z;
        motionIsUsed = true;
        controlledCam = GetComponent<Camera>().GetComponent<Rigidbody2D>();

    }

    public void AllowMovementByRotating(Vector3 centrePoint) {
       
    }

    public void AllowMovementByPanning(Vector2 axis) {

    }

    // Update is called once per frame
    void Update() {
        if (!motionIsUsed) {
            return;
        }
        Vector3 acceleration = Input.acceleration;
        Vector3 filtered = new Vector3(
                xAxis ? acceleration.x : 0,
                yAxis ? acceleration.y : 0,
                zAxis ? acceleration.z : 0
            );
        filtered.x = Mathf.Abs(filtered.x) < .1f ? 0 : filtered.x;
        filtered.y = Mathf.Abs(filtered.y) < .1f ? 0 : filtered.y;
        controlledCam.AddForce(100 * filtered);
        Debug.Log(filtered);
    }
}
