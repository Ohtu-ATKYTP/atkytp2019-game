using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IndicatorRotator : MonoBehaviour
{
    Vector3 rotationVector;

    public void Start() {
        rotationVector = new Vector3(0, 0, 0);
    }
    public void RotateToAngle(float angles) {
        rotationVector.z = angles;
        this.transform.localRotation = Quaternion.Euler(0, 0, -1 * angles);
    }
}
