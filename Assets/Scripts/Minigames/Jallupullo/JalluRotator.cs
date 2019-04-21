using System.Collections;
using UnityEngine;

public class JalluRotator : MonoBehaviour
{
    public IndicatorRotator indicator;

    public void Start() {
        if (!Input.gyro.enabled) {
            Input.gyro.enabled = true;
        }

    }

    public void Initialize() {
        StartCoroutine(CORUpdateRotation());
    }

    public void SetCameraRotationTo(float angle) {
        this.transform.rotation = Quaternion.Euler(0f, 0f, -1 * angle);
    }


    public IEnumerator CORUpdateRotation() {
        Vector3 XYVector = new Vector3(0, 0, 0);
        Quaternion devAttitude;
        while (true) {
            devAttitude = Input.gyro.attitude;
            XYVector.z = devAttitude.eulerAngles.z + 90f;
            transform.eulerAngles = XYVector;
            indicator.RotateToAngle(-1 * XYVector.z);
            yield return new WaitForSecondsRealtime(.25f);
        }
    }
}
