using UnityEngine;

public class GamePaneRotator : MonoBehaviour {
    public Vector3 centerPoint = new Vector3(0, 0, 0);
    public float speed = 10f;
    public bool clockWise = false;
    public bool rotates = true;


    void Update() {
        if (!rotates) {
            return; 
        }
        transform.RotateAround(centerPoint, new Vector3(0, 0, 1), Time.deltaTime * speed);
    }
}
