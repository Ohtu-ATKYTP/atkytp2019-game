using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveMetro : MonoBehaviour
{   
    public Rigidbody2D rb;
    Vector2 swipeStartLocation = new Vector2(9999,9999);

    // Update is called once per frame
    void Update()
    {
        if(swipeStartLocation.x == 9999 && swipeStartLocation.y == 9999 && Input.GetMouseButton(0)){
            swipeStartLocation = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
        }    
        if(swipeStartLocation.x != 9999 && swipeStartLocation.y != 9999 && !Input.GetMouseButton(0)){
            rb.AddForce(new Vector2(Input.mousePosition.x-swipeStartLocation.x,0));
            swipeStartLocation = new Vector2(9999,9999);
        }    
    }
}
