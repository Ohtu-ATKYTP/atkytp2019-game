using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GamePanePanner : MonoBehaviour {
    private Vector3 direction;
    public bool moves = false;
    private float distanceFromOrigin;
    private float timeRemaining;
    public float speed = 10f;

    void Start() {
        //Initialize(new Vector2(1, 0));
    }

    public void Initialize(Vector2 direction, float panningLengthInSecs) {
        timeRemaining = panningLengthInSecs;
        this.direction = Vector3.ClampMagnitude(direction, 1f);
        moves = true;
        if (Application.isEditor) {
            StartCoroutine(UpdateDistanceInfoCOR());
        }
    }

    void Update() {
        if (!moves) {
            return;
        }
        this.transform.position += Time.deltaTime * direction * speed;

        timeRemaining -= Time.deltaTime;
        if(timeRemaining <= 0){ moves = false;}
   
    }

    private void OnDrawGizmos() {
        float distance = Mathf.Max(100f, 2 * distanceFromOrigin);
        Gizmos.color = Color.green;
        Gizmos.DrawLine(-1 * distance * direction, distance * direction);
    }

    private IEnumerator UpdateDistanceInfoCOR() {
        while (true) {
            distanceFromOrigin = Vector3.Magnitude(transform.position);
            yield return new WaitForSecondsRealtime(1f);
        }

    }
}
