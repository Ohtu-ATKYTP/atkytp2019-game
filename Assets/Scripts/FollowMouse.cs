using System.Collections;
using UnityEngine;

public class FollowMouse : MonoBehaviour
{
    void Update()
    {
        Vector3 pos = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition[0], Input.mousePosition[1], Camera.main.transform.position.z * -1));
        pos.z = 0;
        transform.position = pos;
    }
}
