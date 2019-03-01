using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DifficultyAdjuster : MonoBehaviour {
    public TimeProgress timer;
    // Used to determine the difficulty of the map sprite
    public SpriteManager spriteManager;




    public void Initialize(int difficulty) {
        timer = FindObjectOfType<TimeProgress>();
        spriteManager = GameObject.Find("Finland").GetComponent<SpriteManager>();
        timer.seconds = Mathf.FloorToInt(Mathf.Max(1f, 3f - (0.5f * (difficulty - 1))));

        // parameter: difference from the easiest (0) map, higher signifies more difficult
        // if no difficult enough sprite, the most difficult is used
        spriteManager.ChangeSprite(difficulty - 1);
         spriteManager.Flip(Mathf.Min(difficulty, 3));

        //spriteManager.Rotate(30);

    }

}
