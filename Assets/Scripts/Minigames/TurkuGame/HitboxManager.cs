using UnityEngine;

public class HitboxManager : MonoBehaviour { 

    private Hitbox[] hitboxes;
    public TurkuManager manager;
    
    // Collect all hitboxes into an array
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

    // How many hitboxes have been hovered over
    public int Active()
    {
        int i = 0;
        foreach(Hitbox h in this.hitboxes)
        {
            if (h.GetActive()) i++;
        }
        return i;
    }

    // How many hitboxes are there
    public int HitboxCount()
    {
        return this.hitboxes.Length;
    }
}
