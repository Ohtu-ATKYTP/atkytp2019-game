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
    private float? initialZAngle = null;
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
    private float elapsedTime = 0f;
    private float xSpeed = 0;
    private float xPrevAcc = 0;
    private Vector2 initialLinearDirection = Vector2.zero;
    private Vector3 accSumVector = new Vector3(0, 0, 0);

    private Vector3 asymptote = Vector3.zero;


    public void Initialize(Dictionary<string, Vector2> rotationalInfo) {
        Input.gyro.enabled = true;
        Input.compensateSensors = true;
        gamePane = GameObject.FindGameObjectWithTag("GamePane").transform;
        controlledCam = this.gameObject;
        cameraBody = GetComponent<Camera>().GetComponent<Rigidbody2D>();
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
            Instantiate(centerPointPrefab, centerPoint, Quaternion.identity);
            centerPointPrefab.GetComponent<FixedJoint2D>().connectedBody = this.GetComponent<Rigidbody2D>();
            rotationMethod = RotateAroundPoint;
        }

    }

    public void AllowMovementByPanning(Vector2 axis) {
        motionIsUsed = true;
        panningAxis = axis;
        rotationMethod = PanAlongLine;

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
    }

    private void OnDrawGizmos() {
        Gizmos.color = Color.cyan;
        Vector2 force = RotationToForce(Input.gyro.rotationRateUnbiased, 2);
        Vector2 projectedNormalized = ProjectionVector(force, panningAxis).normalized;
        Gizmos.DrawLine(Vector3.zero, 100 * new Vector3(projectedNormalized.x, projectedNormalized.y, 0));

        Gizmos.color = Color.blue;
        Gizmos.DrawLine(Vector3.zero, asymptote);
    }





    // limit line to point to the upper half of the coordinates (so we always know which way is 'up')
    private void PanAlongLine() {
        elapsedTime += Time.deltaTime;

        if (cameraBody.bodyType != RigidbodyType2D.Dynamic) {
            return;
        }

        if (initialLinearDirection != Vector2.zero) {
            return;
        }
        Vector2 force = RotationToForce(Input.gyro.rotationRateUnbiased, .7f);
        Vector2 projectedNormalized = ProjectionVector(force, panningAxis).normalized;
        if (projectedNormalized == Vector2.zero) {
            return;
        }
        cameraBody.AddForce(30 * projectedNormalized);
        initialLinearDirection = projectedNormalized;
    }

    private void FixedUpdate() {
        if (elapsedTime > .2f && Vector3.Distance(new Vector3(this.transform.position.x, this.transform.position.y, 0),
  new Vector3(gamePane.position.x, gamePane.position.y, 0)) < .1f) {
            cameraBody.bodyType = RigidbodyType2D.Static;
        }
    }

    private Vector2 ProjectionVector(Vector2 projected, Vector2 projectee) {
        return (Vector2.Dot(projected, projectee) / projectee.sqrMagnitude) * projectee;
    }
    /*
     * Returns something from {-1, 0, 1} x {-1, 0, 1} so that the coordinates match the game world coordinates
     *  (force (1, 0) applies force from directly down to directly up)
     * 
     */
    private Vector2 RotationToForce(Vector2 rotations, float threshold) {
        float forceX = 0;
        if (Mathf.Abs(rotations.y) > threshold) {
            forceX = rotations.y > 0 ? 1 : -1;
        }

        float forceY = 0;
        if (Mathf.Abs(rotations.x) > threshold) {
            forceY = rotations.x > 0 ? -1 : 1;
        }
        return new Vector2(forceX, forceY);
    }

    private Vector2 RotationToAsympForce(float zRotation, Vector2 intersection, Vector2 origin, float threshold = .7f) {
        if (Mathf.Abs(zRotation) <= threshold) {
            return Vector2.zero;
        }
        Vector2 ray = intersection - origin;
        if (ray.y == 0) {
            throw new UnityException("Should not be possible: origin of the circle should be removed from origin of the world space.");
        }
        Vector2 result = new Vector2(ray.y, (-1 * ray.y * ray.x) / ray.y);

        if (result.x > 0) {
            result *= -1;
        }
        if (zRotation < 0) {
            result *= -1;
        }
        result = result.normalized;
        asymptote = result;
        return result;
    }


    private void RotateAroundPoint() {
        elapsedTime += Time.deltaTime;
        if (initialZAngle != null) {
            return;
        }
        Vector2 forceVector = RotationToAsympForce(Input.gyro.rotationRateUnbiased.z, this.transform.position, paneRotator.centerPoint);

        float rate = -1 * Input.gyro.rotationRateUnbiased.z;
        if (forceVector.sqrMagnitude < .1f) {
            return;
        }
        initialZAngle = rate;
        cameraBody.AddForce(30 * forceVector);

    }

    private void RotateInPlace() {
        if (!motionIsUsed) {
            return;
        }

        if (paneRotator.timeRemaining <= 0 && Quaternion.Angle(transform.rotation, gamePane.rotation) < 1f) {
            motionIsUsed = false;
            return;
        }

        if (initialZAngle == null) {
            initialZAngle = Input.gyro.attitude.eulerAngles.z + 90f;
        }

        float relativeZAngle = initialZAngle.Value + Input.gyro.attitude.eulerAngles.z;
        cameraBody.MoveRotation(relativeZAngle);

    }




}
