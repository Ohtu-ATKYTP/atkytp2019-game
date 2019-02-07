using System.Collections;
using UnityEngine;

public class FollowMouse : MonoBehaviour
{
    void Update()
    {
        Vector3 p = new Vector3(Input.mousePosition[0], Input.mousePosition[1], Camera.main.transform.position.z*-1);
        Vector3 pos = Camera.main.ScreenToWorldPoint(p);
        pos.z = 0;
        Debug.Log(p+" -- "+pos);
        transform.position = pos;
    }
}
