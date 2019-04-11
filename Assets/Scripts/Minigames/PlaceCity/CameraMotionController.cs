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
    private float initialZAngle;
    private float initialXAngle; 
    private Vector3 initialVector;
    private GamePaneRotator paneRotator;
    private GamePanePanner panePanner;
    private Vector2 centerPoint;
    private float continuousRotation = 0f;
    private bool rotating = false;
    private int? direction = null;
    private Transform gamePane;
    private bool initPushGiven = false;
    private Vector2 panningAxis; 


    public void Initialize(Dictionary<string, Vector2> rotationalInfo) {
                Input.compensateSensors = true;
        gamePane = GameObject.FindGameObjectWithTag("GamePane").transform;
        controlledCam = this.gameObject;
        cameraBody = GetComponent<Camera>().GetComponent<Rigidbody2D>();
        initialZAngle = Input.gyro.attitude.eulerAngles.z - 90f;
        initialXAngle = Input.gyro.attitude.eulerAngles.x;
        Debug.Log("Initial: " + initialZAngle);

        paneRotator = FindObjectOfType<GamePaneRotator>();
        panePanner = FindObjectOfType<GamePanePanner>();
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
            this.centerPoint = centerPoint;
            rotationMethod = RotateAroundPoint;
        }

    }

    public void AllowMovementByPanning(Vector2 axis) {
        motionIsUsed = true;
        panningAxis = axis;
        rotationMethod = PanAlongLine;
    }

    // Update is called once per frame
    void Update() {
        if (!motionIsUsed) {
            return;
        }

        rotationMethod();
    }

    // limit line to point to the upper half of the coordinates (so we always know which way is 'up')
    private void PanAlongLine() {
        float rate = initialXAngle - Input.gyro.attitude.eulerAngles.x; 
        if (!rotating && continuousRotation < 0.5f) {
            if (rate > 30) {
                rotating = true;
                direction = 1;
            } else if (rate < -30f) {
                rotating = true;
                direction = -1;
            }
        } else if (!paneRotator.rotates && Vector3.Distance(new Vector3(this.transform.position.x, this.transform.position.y, 0),
                 new Vector3(gamePane.position.x, gamePane.position.y, 0)) < .8f) {
            rotating = false;
        } else {
            this.transform.position += Time.deltaTime * direction.Value * panePanner.speed * 1.1f * Vector3.ClampMagnitude(panningAxis, 1f);
            continuousRotation += Time.deltaTime;
        }
    }


    private void RotateAroundPoint() {

        float rate = initialZAngle - Input.gyro.attitude.eulerAngles.z + 90f;
        if (!rotating && continuousRotation < 0.5f) {
            if (rate > 30) {
                rotating = true;
                direction = 1;
            } else if (rate < -30f) {
                rotating = true;
                direction = -1;
            }
        } else if (!paneRotator.rotates && Vector3.Distance(new Vector3(this.transform.position.x, this.transform.position.y, 0),
                 new Vector3(gamePane.position.x, gamePane.position.y, 0)) < .8f) {
            rotating = false;
        } else {
            transform.RotateAround(centerPoint, new Vector3(0, 0, 1), direction.Value * Time.deltaTime * paneRotator.speed * 1.3f);
            continuousRotation += Time.deltaTime;
        }
    }

    private void RotateInPlace() {

        float relativeZAngle = initialZAngle + Input.gyro.attitude.eulerAngles.z;
        cameraBody.MoveRotation(relativeZAngle);
        if (Quaternion.Angle(transform.rotation, gamePane.rotation) < 5f) {
            motionIsUsed = false;
        }
    }
}
