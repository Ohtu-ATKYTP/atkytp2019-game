using UnityEngine;

public class MoveMetroTouch : MonoBehaviour
{   
    public Rigidbody2D rb;
    public int difficulty;
    Vector3 swipeStartLocation;
    Vector3 currentSwipeLocation;
    bool newSwipe = true;

    // Update is called once per frame
    void Update()
    {
        if(newSwipe && Input.touchCount != 0){
            swipeStartLocation = Input.GetTouch(0).position;
            newSwipe = false;
        }
        if(!newSwipe && Input.touchCount != 0){
            currentSwipeLocation = Input.GetTouch(0).position;
        }    
        if(!newSwipe && Input.touchCount == 0){
            rb.AddForce((currentSwipeLocation-swipeStartLocation)/difficulty);
            newSwipe = true;
        }    
    }
}
