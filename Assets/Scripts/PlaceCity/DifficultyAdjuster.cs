using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DifficultyAdjuster : MonoBehaviour {
    public TimeProgress timer;
    // Used to determine the difficulty of the map sprite
    public SpriteManager spriteManager;
    public float shortestPossibleTime = 1f;
    public int steps = 10;



    public void Initialize(int difficulty) {
        timer = FindObjectOfType<TimeProgress>();
        spriteManager = GameObject.Find("Finland").GetComponent<SpriteManager>();
        // parameter: difference from the easiest (0) map, higher signifies more difficult
        // if no difficult enough sprite, the most difficult is used

        TuneTime(difficulty);
        TuneSprite(difficulty);
        TuneRotation(difficulty);
        TuneFlipping(difficulty); 

    }

    private void TuneTime(int difficulty) {
        // aika laskee lineaarisesti: joka vaikeustason nousulla
        // 1 / askelia  * | pisin - lyhin | vähemmän aikaa
        timer.seconds = (timer.seconds - ( Mathf.Floor(difficulty / 2) / steps))
            +  shortestPossibleTime;

        if (timer.seconds < shortestPossibleTime) {
            timer.seconds = shortestPossibleTime;
        }
    }

    private void TuneSprite(int difficulty) {
        if (difficulty > 4) {
            spriteManager.ChangeSprite(2); 
        } else if (difficulty > 1) {
            spriteManager.ChangeSprite(1);
        }
    }

    private void TuneRotation(int difficulty) {

            
    }

    private void TuneFlipping(int difficulty) {
        if (difficulty > 10) {
            spriteManager.Flip(true, true); 
        } else if (difficulty > 7) {
            spriteManager.Flip(false, true); 
        } else if (difficulty > 5) {
            spriteManager.Flip(true, false); 
        }        
    }

}
