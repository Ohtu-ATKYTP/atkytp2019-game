using UnityEngine;

public class ContinuousRotation : MonoBehaviour
{
    public int degreesPerSecond = 90;
    public bool clockWise = false;
    private Vector3 rotationVector = new Vector3(0, 0, 0);

    void Update()
    {

            rotationVector.z =
                (clockWise ? -1 : 1)
                * Time.deltaTime * degreesPerSecond;

            transform.Rotate(rotationVector);
        
    }
    
}
