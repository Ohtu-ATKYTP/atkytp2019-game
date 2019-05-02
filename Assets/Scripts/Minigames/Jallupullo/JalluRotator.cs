using System.Collections;
using UnityEngine;

public class JalluRotator : MonoBehaviour
{
    public IndicatorRotator indicator;

    public void Start() {
        if (!Input.gyro.enabled) {
            Input.gyro.enabled = true;
        }
        Input.compensateSensors = true;


    }

    public void Initialize() {
        StartCoroutine(CORUpdateRotation());
    }

    public void SetCameraRotationTo(float angle) {
        this.transform.rotation = Quaternion.Euler(0f, 0f, -1 * angle);
    }


    public IEnumerator CORUpdateRotation() {

        Vector3 XYVector = new Vector3(0, 0, 0);
        Vector3 gravity;
        Vector2 down = new Vector2(0, -1);
        Vector2 gravity2D = new Vector2(0, 0);
        float angle;
        while (true) {
            gravity = Input.gyro.gravity;
            if(Mathf.Abs(gravity.z) <= .5f)
            {
                gravity2D.x = gravity.x;
                gravity2D.y = gravity.y;
                angle = Vector2.SignedAngle( gravity2D, down);
                XYVector.z = angle;
                transform.eulerAngles = XYVector;
                indicator.RotateToAngle(-1 * XYVector.z);
            }
            yield return new WaitForSecondsRealtime(.2f);
        }
    }
}
