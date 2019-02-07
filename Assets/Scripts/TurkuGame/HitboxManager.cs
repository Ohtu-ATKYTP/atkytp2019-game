using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitboxManager : MonoBehaviour { 

    private Hitbox[] hitboxes;
    
    void Start()
    {
        hitboxes = new Hitbox[transform.GetChildCount()];
        int i = 0;
        foreach (Transform child in transform)
        {
            hitboxes[i] = child.GetComponent<Hitbox>();
            i++;
        }
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(this.active() + " / " + this.maxAmount());
    }

    private int active()
    {
        int i = 0;
        foreach(Hitbox h in this.hitboxes)
        {
            if (h.getActive()) i++;
        }
        return i;
    }

    private int maxAmount()
    {
        return this.hitboxes.Length;
    }
}
