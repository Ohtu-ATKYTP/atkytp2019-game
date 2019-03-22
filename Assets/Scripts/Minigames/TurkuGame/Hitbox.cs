using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hitbox : MonoBehaviour
{
    private bool activated = false;

    void OnMouseEnter()
    {
        this.activated = true;
    }

    public bool getActive() {
        return this.activated;
    }
}
