using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowTouch : MonoBehaviour
{
    public ParticleSystem p;
    public TurkuManager t;
    void Update()
    {
        if(!t.GetLineUsedUp()){
            var e = p.emission;
            if(p.particleCount==t.GetMaxLineLength()){
                t.LineUsedUp();
                e.enabled = false;
            }
            if(Input.touchCount == 0){
                e.enabled = false;
            }else{
                StartCoroutine(delay());
                Touch touch = Input.GetTouch(0);
                Vector3 pos = Camera.main.ScreenToWorldPoint(new Vector3(touch.position.x, touch.position.y, Camera.main.transform.position.z * -1));
                pos.z = 0;
                transform.position = pos;
            }
        }
    }

    IEnumerator delay(){
        yield return new WaitForSecondsRealtime(0.001f);
        var e = p.emission;
        e.enabled = true;
    }
}
