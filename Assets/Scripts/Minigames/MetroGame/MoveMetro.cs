using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveMetro : MonoBehaviour
{   
    public Rigidbody2D rb;
    public int difficulty;
    Vector3 swipeStartLocation;
    bool newSwipe = true;

    // Update is called once per frame
    void Update()
    {
        if(newSwipe && Input.GetMouseButton(0)){
            swipeStartLocation = Input.mousePosition;
            newSwipe = false;
        }    
        if(!newSwipe && !Input.GetMouseButton(0)){
            rb.AddForce((Input.mousePosition-swipeStartLocation)/difficulty);
            newSwipe = true;
        }    
    }
}
