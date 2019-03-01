using System.Collections;
using UnityEngine;

public class FollowMouse : MonoBehaviour
{
    public ParticleSystem p;

    void Update()
    {
        var e = p.emission;
        if(!Input.GetMouseButton(0)){
            e.enabled = false;
        }else{
            StartCoroutine(delay());
            Vector3 pos = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition[0], Input.mousePosition[1], Camera.main.transform.position.z * -1));
            pos.z = 0;
            transform.position = pos;
        }
    }

    IEnumerator delay(){
        yield return new WaitForSecondsRealtime(0.001f);
        var e = p.emission;
        e.enabled = true;
    }
}
