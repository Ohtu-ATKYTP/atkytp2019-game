using UnityEngine;

public class InputController : MonoBehaviour {
    private DeviceType playingDevice;
    private System.Func<Vector3?> clickingMethod;
    private bool interactingAllowed;

    void Start() {
        playingDevice = SystemInfo.deviceType;
        if (playingDevice == DeviceType.Handheld) {
            clickingMethod = touchInputLocation;
        } else if (playingDevice == DeviceType.Desktop) {
            clickingMethod = cursorInputLocation;
        } else {
            throw new UnityException("The device type is unknown");
        }
        interactingAllowed = true;
       
    }


    void Update() {
        Vector3? pos = clickingMethod();
        if (!interactingAllowed || pos == null) {
            return;
        }
        RaycastHit2D hit;
        hit = Physics2D.Raycast(pos.Value, Vector2.zero);
        if (!hit) {
            return;
        }
        GameObject city = hit ? hit.collider.gameObject : null;
        FindObjectOfType<PlaceCityManager>().handleCityInteraction(city);
        interactingAllowed = false;
    }

    private Vector3? touchInputLocation() {
        return inputLocation(Input.touches.Length > 0, () => {
                Touch touch = Input.GetTouch(0);                
                return new Vector3(touch.position.x, touch.position.y, 0);
            });
    }

    private Vector3? cursorInputLocation() {
        return inputLocation(Input.GetMouseButtonUp(0), () => Input.mousePosition);
    }


    private Vector3? inputLocation(bool condition, System.Func<Vector3> positionSeeker) {
        if (condition) {
            Vector3 pos = positionSeeker();
            return Camera.main.ScreenToWorldPoint(pos);
        }
        return null;
    }


}
