using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class InformationDisplayer : MonoBehaviour
{
    public int scaleFactor = 80;
    public bool displayPosition = true;
    private SpriteRenderer spriteRenderer;
    public GameObject finishPrefab;

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

        GameObject go = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        go.transform.parent = this.transform;
        go.transform.localPosition = new Vector3(0, 0, 0);
        go.transform.localScale = new Vector3(10, 10, 1);

        // Color color = correctCity ? Color.green : Color.red;
        // spriteRenderer.color = color;
        // DisplayOnMap();
    }




    public void RevealOnMap(Color color) {


        GameObject go = Instantiate(finishPrefab);
        go.transform.parent = this.transform;
        go.transform.localPosition = new Vector3(0, 0, 0);
        go.GetComponent<SpriteRenderer>().color = color;
        go.transform.localScale = new Vector3(1.5f, 1.5f, .1f);

        spriteRenderer.enabled = false;


    }
}
