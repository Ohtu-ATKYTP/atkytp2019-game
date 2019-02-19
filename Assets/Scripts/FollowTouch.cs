using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowTouch : MonoBehaviour
{
    void Start()
    {
        
    }

    void Update()
    {
        if(Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            Vector3 pos = Camera.main.ScreenToWorldPoint(new Vector3(touch.position.x, touch.position.y, Camera.main.transform.position.z * -1));
            pos.z = 0;
            transform.position = pos;
        } else
        {
            transform.position = new Vector3(3000, 3000, 0);
        }
    }
}
