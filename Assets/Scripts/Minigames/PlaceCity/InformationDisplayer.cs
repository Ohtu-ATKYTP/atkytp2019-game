using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class InformationDisplayer : MonoBehaviour {
    public int scaleFactor = 80;
    public bool displayPosition = true;
    private SpriteRenderer spriteRenderer;

    void Awake() {
        spriteRenderer = GetComponent<SpriteRenderer>();
        if (!Application.isEditor || !displayPosition) {
            spriteRenderer.enabled = false;
        }
    }



    public void DisplayOnMap() {
        spriteRenderer.enabled = true;
    }

    public void DisplayOnMap(bool correctCity) {
        Color color = correctCity ? Color.green : Color.red;
        spriteRenderer.color = color;
        DisplayOnMap();
    }




    public void RevealOnMap(Color color) {
        spriteRenderer.enabled = true;
        spriteRenderer.color = color;
        this.transform.localScale += new Vector3(scaleFactor, scaleFactor, 0);
    }
}
