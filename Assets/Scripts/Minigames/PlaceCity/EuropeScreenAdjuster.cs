using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EuropeScreenAdjuster : MonoBehaviour
{
    public Transform finlandTransform;
    private SpriteRenderer spriteRenderer;
    private Vector3 toOrigin; 
    private float deltaX;
    private float deltaY;

    // Start is called before the first frame update
    void Start() {
        Initialize();
    }


    public void Initialize() {
        spriteRenderer = GetComponent<SpriteRenderer>();
        Vector3 finlandWorldPosition = finlandTransform.position;
        Vector3 ownWorldPosition = transform.position;
        Vector3 center = spriteRenderer.bounds.center; 

        // In what space is the center point
        toOrigin = center - finlandWorldPosition; 
        deltaX = toOrigin.x;
        deltaY = toOrigin.y;
    }

    public void Flip(bool x, bool y) {
        if (spriteRenderer == null) {
            Initialize(); 
        }
        spriteRenderer.flipX = x;
        spriteRenderer.flipY = y; 

        transform.position += new Vector3(x ? -2 * deltaX : 0, y ? -2  * deltaY : 0, 0);
    }
}
