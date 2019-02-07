using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaceCityManager : MonoBehaviour {
    public Transform[] locations;
    public SpriteRenderer map;
    public float radius = .1f;
  

    void Start() {
    }

    private void Initialize() {
        Start();
    }




    void OnDrawGizmos() {
        Initialize();
        Gizmos.color = Color.cyan;
        for (int i = 0; i < locations.Length; i++) {
            Vector3 v = locations[i].position;
            Gizmos.DrawSphere(map.transform.TransformVector(v + map.transform.position), radius);
            Debug.Log(v);
        }
    }

    void Update() {

    }
}
