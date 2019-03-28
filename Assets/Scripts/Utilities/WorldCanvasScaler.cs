
using UnityEngine;

/* 
* This should be attached to a world-space canvas that is a child of a camera (in relation to which the canvas will be scaled / moved)
* 
* Scale should also be set.... appropriately (see place city game or await documentation)
* 
*/
[ExecuteInEditMode]
public class WorldCanvasScaler : MonoBehaviour {
    private Vector2 sizeDelta;
    private bool initialized = false;



    void Awake() {
        Camera relatedCamera = GetComponentInParent<Camera>();
        if (!relatedCamera.orthographic) {
            Debug.LogError("Only ortographic cameras supported at the moment");
            return;
        }
        float halfSize = relatedCamera.orthographicSize;
        RectTransform rt = GetComponent<RectTransform>();
        float canvasHeight = 2 * halfSize * 100;
        float canvasWidth = relatedCamera.aspect * canvasHeight;
        rt.sizeDelta = new Vector2(canvasWidth, canvasHeight);

    }

    private void Initialize() {
        initialized = true;
    }

    private void Update() {
        if (!initialized) {
            Initialize();
        }

    }

}
