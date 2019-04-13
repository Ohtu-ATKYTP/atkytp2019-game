using UnityEngine;

public class GamePaneRotator : MonoBehaviour {
    public Vector2 centerPoint;
    public float speed;
    public bool rotates = false;
    public float timeRemaining;
    public bool clockWise; 

    public void Initialize(Vector2 centerPoint, float speed = 2f, bool clockWise = true, float rotationLengthInSecs = 3f) {
        this.clockWise = clockWise;
        this.timeRemaining = rotationLengthInSecs;
        this.centerPoint = centerPoint;
        this.speed = (clockWise ? -1 : 1) * speed;
        rotates = true;
    }

    void Update() {
        if (!rotates) {
            return;
        }
        transform.RotateAround(centerPoint, new Vector3(0, 0, 1), Time.deltaTime * speed);      

        timeRemaining -= Time.deltaTime;
        if (timeRemaining <= 0) {
            rotates = false;
        }
    }
}
