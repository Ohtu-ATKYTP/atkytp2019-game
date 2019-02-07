using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisplayInEditor : MonoBehaviour {
    public Color drawColor = Color.cyan;
    public Camera camera;
    private bool isInitialized = false; 
    private Vector3 leftBottom;
    private Vector3 leftTop;
    private Vector3 rightBottom;
    private Vector3 rightTop;

    void Start() {

    }

    private void initializePoints(){
        
        leftBottom = camera.ScreenToWorldPoint(new Vector3(0, 0, 0));
        rightTop = camera.ScreenToWorldPoint(new Vector3(camera.pixelWidth, camera.pixelHeight, 0));
        leftTop = new Vector3(leftBottom.x, rightTop.y, 0);
        rightBottom = new Vector3(rightTop.x, leftBottom.y, 0);
      }

    void OnDrawGizmos() {
        if(!isInitialized){
            initializePoints();
            isInitialized = true;
         }



        Gizmos.color = Color.cyan;
        Gizmos.DrawLine(leftBottom, leftTop);
        Gizmos.DrawLine(leftTop, rightTop);
        Gizmos.DrawLine(rightTop, rightBottom);
        Gizmos.DrawLine(rightBottom, leftBottom);
    }

}
