using UnityEngine;

public class Waves : MonoBehaviour
{
    float i = (3/2)*Mathf.PI;

    void Update()
    {
        float amount = Mathf.Sin(i+Time.deltaTime) - Mathf.Sin(i);
        i+=Time.deltaTime;
        transform.Translate(0,amount,0);
    }
}
