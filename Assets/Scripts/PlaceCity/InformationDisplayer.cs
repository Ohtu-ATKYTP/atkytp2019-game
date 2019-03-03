using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class InformationDisplayer : MonoBehaviour {
    public int scaleFactor = 80;
    public bool displayCollider = true; 
    private SpriteRenderer spriteRenderer;
    private CircleCollider2D collider;




    void Awake() {
        spriteRenderer = GetComponent<SpriteRenderer>();
        collider = GetComponent<CircleCollider2D>();
        if (!Application.isEditor) {
            spriteRenderer.enabled = false;
        }
    }



    public void DisplayOnMap() {
        spriteRenderer.enabled = true; 
    }


    

    public void RevealOnMap(Color color){
        spriteRenderer.enabled = true;
        spriteRenderer.color = color;
        this.transform.localScale += new Vector3(scaleFactor, scaleFactor, 0);
    }
}
