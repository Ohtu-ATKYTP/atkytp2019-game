using UnityEngine;

public class GamePaneRotator : MonoBehaviour {
    public Vector2 centerPoint;
    public float speed = 10f;
    public bool rotates = false;


    public void Initialize(Vector2 centerPoint, float speed = 10f, bool clockWise = true) {
        this.centerPoint = centerPoint;
        this.speed = (clockWise ? -1 : 1) * speed;
        rotates = true;
    }

    void Update() {
        if (!rotates) {
            return; 
        }
        transform.RotateAround(centerPoint, new Vector3(0, 0, 1), Time.deltaTime * speed);
        
    }
}
