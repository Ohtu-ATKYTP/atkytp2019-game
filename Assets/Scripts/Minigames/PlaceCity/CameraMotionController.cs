using System.Collections.Generic;
using System.Collections;
using UnityEngine;

public class CameraMotionController : MonoBehaviour {
    public bool motionIsUsed = false;
    private Rigidbody2D cameraBody;
    public GameObject centerPointPrefab;
    private System.Action rotationMethod;
    private float? initialZAngle = null;
    private GamePaneRotator paneRotator;
    private GamePanePanner panePanner;
    private Transform gamePane;
    private Vector2 panningAxis;
    private float elapsedTime = 0f;
#if UNITY_EDITOR
    private Vector3 asymptote = Vector3.zero;
#endif


    public void Initialize(Dictionary<string, Vector2> rotationalInfo) {
        if (rotationalInfo == null) {
            throw new System.ArgumentNullException("Information about movement cannot be null");
        }

        gamePane = GameObject.FindGameObjectWithTag("GamePane").transform;
        paneRotator = gamePane.gameObject.GetComponent<GamePaneRotator>();
        panePanner = gamePane.gameObject.GetComponent<GamePanePanner>();
        cameraBody = GetComponent<Rigidbody2D>();


        Input.gyro.enabled = true;
        Input.compensateSensors = true;

        foreach (string movementType in rotationalInfo.Keys) {
            if (movementType == "rotation") {
                AllowMovementByRotating(rotationalInfo[movementType]);
            } else if (movementType == "panning") {
                AllowMovementByPanning(rotationalInfo[movementType]);
            }
        }
    }

    private void AllowMovementByRotating(Vector2 centerPoint) {
        motionIsUsed = true;
        if (centerPoint == Vector2.zero) {
            rotationMethod = RotateInPlace;
            cameraBody.constraints = RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezePositionY;
        } else {
            GameObject centerSphere = Instantiate(centerPointPrefab, centerPoint, Quaternion.identity);
            centerSphere.GetComponent<FixedJoint2D>().connectedBody = cameraBody;
            rotationMethod = RotateAroundPoint;
        }

    }

    private void AllowMovementByPanning(Vector2 axis) {
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
        elapsedTime += Time.deltaTime;
        rotationMethod();
    }


#if UNITY_EDITOR
    private void OnDrawGizmos() {
        Gizmos.color = Color.cyan;
        Vector2 force = RotationToForce(Input.gyro.rotationRateUnbiased, 2);
        Vector2 projectedNormalized = ProjectionVector(force, panningAxis).normalized;
        Gizmos.DrawLine(Vector3.zero, 100 * new Vector3(projectedNormalized.x, projectedNormalized.y, 0));

        Gizmos.color = Color.blue;
        Gizmos.DrawLine(Vector3.zero, asymptote);
    }
#endif





    private void PanAlongLine() {
        if (cameraBody.bodyType != RigidbodyType2D.Dynamic) {
            return;
        }
        Vector2 force = RotationToForce(Input.gyro.rotationRateUnbiased, .7f);
        Vector2 projectedNormalized = ProjectionVector(force, panningAxis).normalized;
        if (projectedNormalized == Vector2.zero) {
            return;
        }
        cameraBody.AddForce(75 * projectedNormalized);
        motionIsUsed = false;
    }

    private void FixedUpdate() {
        // Avoid calculating anything unnecessary in such a tight loop
        if (cameraBody != null && cameraBody.bodyType != RigidbodyType2D.Dynamic) {
            return;
        }

        if (elapsedTime > .5f &&
            (new Vector2(this.transform.position.x - gamePane.position.x, this.transform.position.y - gamePane.position.y).sqrMagnitude <= .25f)) {

            StartCoroutine(CORSettleFinalAdjustments());
            cameraBody.bodyType = RigidbodyType2D.Static;
            paneRotator.rotates = false;
            panePanner.moves = false;
            motionIsUsed = false;


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
    private Vector2 RotationToForce(Vector2 rotations, float threshold = .7f) {
        Vector2 forceVector = new Vector2(0, 0);
        if (Mathf.Abs(rotations.y) > threshold) {
            forceVector.x = rotations.y > 0 ? -1 : 1;
        }

        if (Mathf.Abs(rotations.x) > threshold) {
            forceVector.y = rotations.x > 0 ? -1 : 1;
        }

        return forceVector;
    }

    private Vector2 RotationToAsympForce(float zRotation, Vector2 intersection, Vector2 origin, float threshold = .7f) {
        Vector2 forceVector = new Vector2(0, 0);
        if (Mathf.Abs(zRotation) <= threshold) {
            return forceVector;
        }
        Vector2 ray = intersection - origin;
        Vector2 result;
        // if ray parallel to y-axis, a parallel to x-axis will do
        if (ray.y == 0) {
            result = new Vector2(-1, 0);
        } else {
            result = new Vector2(ray.y, (-1 * ray.y * ray.x) / ray.y);
        }


        if (result.x > 0) {
            result *= -1;
        }
        if (zRotation < 0) {
            result *= -1;
        }

        result = result.normalized;
#if UNITY_EDITOR
        asymptote = result;
#endif 
        return result;
    }


    private void RotateAroundPoint() {
        Vector2 forceVector = RotationToAsympForce(Input.gyro.rotationRateUnbiased.z, this.transform.position, paneRotator.centerPoint);
        if (forceVector.sqrMagnitude < .1f) {
            return;
        }
        cameraBody.AddForce(75 * forceVector);
        motionIsUsed = false;
    }

    private void RotateInPlace() {
        if (paneRotator.timeRemaining <= 0 && Quaternion.Angle(transform.rotation, gamePane.rotation) < 1f) {
            motionIsUsed = false;
            return;
        }

        if (initialZAngle == null) {
            initialZAngle = Input.gyro.attitude.eulerAngles.z + 90f;
        }
        float relativeZAngle = initialZAngle.Value + Input.gyro.attitude.eulerAngles.z - 90f;
        cameraBody.MoveRotation(relativeZAngle);
    }


    private IEnumerator CORSettleFinalAdjustments() {
        yield return null;
        Vector3 targetPositionInRightZPlane = new Vector3(gamePane.transform.position.x, gamePane.transform.position.y, this.transform.position.z);

        bool transformsFine = Vector3.SqrMagnitude(this.transform.position - targetPositionInRightZPlane) < .01f;
        bool rotationsFine = Quaternion.Angle(this.transform.rotation, gamePane.transform.rotation) < .1f;
        float t = 0f;
        float lengthOfJourney = 2f; 
        

        while (!rotationsFine || !transformsFine) {
            this.transform.rotation = Quaternion.Slerp(this.transform.rotation, gamePane.transform.rotation, t / lengthOfJourney);
            this.transform.position = Vector3.Slerp(this.transform.position, targetPositionInRightZPlane, t / lengthOfJourney);
            t += Time.deltaTime;
            yield return null;
        }

    }
}
