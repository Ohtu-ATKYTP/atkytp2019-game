using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class InformationDisplayer : MonoBehaviour {
    public int scaleFactor = 80;
    public bool displayCollider = true; 
    private SpriteRenderer spriteRenderer;
    private CircleCollider2D collider;




    void Start() {
        spriteRenderer = GetComponent<SpriteRenderer>();
        collider = GetComponent<CircleCollider2D>();
       // Lopulta ei haluta näyttää sijaintia
       // spriteRenderer.enabled = false; 
    }

    void Update() {

    }

    public void DisplayOnMap(){
        spriteRenderer.enabled = true; 
        this.transform.localScale += new Vector3(scaleFactor, scaleFactor, 0);
    }
}
