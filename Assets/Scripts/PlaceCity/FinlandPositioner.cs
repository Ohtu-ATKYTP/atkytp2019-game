using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinlandPositioner : MonoBehaviour {
    [Tooltip("Integer percents relative to the size of the screen")]
    public int distanceFromCorner = 1; 


    // Start is called before the first frame update
    void Start() {
        float distance = isVeryWide() ? distanceFromCorner / 2 : distanceFromCorner;
        Transform t = GetComponent<Transform>();
        SpriteRenderer r = GetComponent<SpriteRenderer>();
        int cameraWidth = Camera.main.pixelWidth;
        int cameraHeight = Camera.main.pixelHeight;        
        int cameraX = (int)(((100 - distance) / 100f) * cameraWidth); 
        int cameraY = (int)((distance / 100f) * cameraHeight);
        Vector3 bottomRightInWorld = Camera.main.ScreenToWorldPoint(new Vector2(cameraX, cameraY));
        float x = bottomRightInWorld.x - r.bounds.extents.x;
        float y = bottomRightInWorld.y + r.bounds.extents.y;


        t.position = new Vector3(x, y, t.position.z);
    }



    //Pitäsikö olla aina muistissa olevassa skenessä apuvälineenä?
    private bool isVeryWide(){
        Debug.Log((float)Screen.width / Screen.height);
 
        return ((float)Screen.width) / Screen.height < (9f / 16); 
        }


}
