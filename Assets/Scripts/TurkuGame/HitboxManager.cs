using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitboxManager : MonoBehaviour { 

    private Hitbox[] hitboxes;
    public TurkuAnimation turkuAnimation;
    void Start()
    {
        hitboxes = new Hitbox[transform.childCount];
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
        if (this.active()/this.maxAmount() > 0.4)
        {
            this.win();
        }
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

    private void win()
    {
        turkuAnimation.StartAnimation();
        Debug.Log("VOITIT PELIN!");
    }
}
