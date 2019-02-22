using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinlandPositioner : MonoBehaviour {
    [Tooltip("Relative distance from the right corner")]
    public float distanceFromCorner = .01f; 


    // Start is called before the first frame update
    void Start() {
        float distance = isVeryWide() ? distanceFromCorner / 2 : distanceFromCorner;
        Transform t = GetComponent<Transform>();
        SpriteRenderer r = GetComponent<SpriteRenderer>();

        int cameraWidth = Camera.main.pixelWidth;
        int cameraHeight = Camera.main.pixelHeight;        
        int cameraX = (int)((1 - distance) * cameraWidth); 
        int cameraY = (int)(distance * cameraHeight);

        Vector3 bottomRightInWorld = Camera.main.ScreenToWorldPoint(new Vector2(cameraX, cameraY));

        float x = bottomRightInWorld.x - r.bounds.extents.x;
        float y = bottomRightInWorld.y + r.bounds.extents.y;

        t.position = new Vector3(x, y, t.position.z);
    }



    //Pitäsikö olla aina muistissa olevassa skenessä apuvälineenä?
    private bool isVeryWide(){ 
        return ((float)Screen.width) / Screen.height < (9f / 16); 
        }


}
