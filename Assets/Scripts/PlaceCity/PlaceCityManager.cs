using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaceCityManager : MonoBehaviour {
    public Transform[] locations;
    public SpriteRenderer map;
    public float radius = 1f;
    public bool drawGizmos = true;
    public Color gizmoColor = Color.cyan;
    private GameManager gameManager; 
  

    void Start() {
        gameManager = FindObjectOfType<GameManager>();
        for(int i = 0; i < locations.Length; i++){
            locations[i].GetComponent<CircleCollider2D>().radius = 2 * radius;        
        }
    }

    private void Initialize() {
        Start();
    }


    void OnDrawGizmos() {
        if(!drawGizmos){
            return;
        }
        Initialize();
        for (int i = 0; i < locations.Length; i++) {
            Vector3 v = locations[i].position;
            Gizmos.DrawSphere(v, radius);
        }
    }

    void Update() {
        if(Input.touchCount > 0){
            Touch touch = Input.GetTouch(0);
            // z = distance from camera
            Vector3 pos = new Vector3(touch.position.x, touch.position.y, 0);
            pos = Camera.main.ScreenToWorldPoint(pos);
            RaycastHit2D hit; 
            hit = Physics2D.Raycast(pos, Vector2.zero);
            
            if(hit){
                Debug.Log("HIT! " + hit.collider.gameObject.name);
            } else { 
               Debug.Log("HÄVISIT PELIN");
                Destroy(this.gameObject);
            }            
        }
    }
}
