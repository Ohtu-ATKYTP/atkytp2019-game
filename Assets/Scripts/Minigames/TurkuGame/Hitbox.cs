using UnityEngine;

public class Hitbox : MonoBehaviour
{
    private bool activated = false;

    void OnMouseEnter()
    {
        this.activated = true;
    }

    public bool GetActive() {
        return this.activated;
    }
}
