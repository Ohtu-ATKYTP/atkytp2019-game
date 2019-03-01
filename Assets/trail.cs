using System.Collections;
using UnityEngine;

public class trail : MonoBehaviour
{   
    public TrailRenderer t;
    TrailRenderer ct;

    void Update()
    {
        if(!Input.GetMouseButton(0)){
            ct = Instantiate(t);
            t.emitting = false;
            Destroy(t);
            t = ct;
        }else{
            t.emitting = true;
            Vector3 pos = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition[0], Input.mousePosition[1], Camera.main.transform.position.z * -1));
            pos.z = 0;
            transform.position = pos;
        }
    }
}
