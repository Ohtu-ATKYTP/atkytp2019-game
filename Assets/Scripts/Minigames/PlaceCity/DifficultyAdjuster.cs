using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/**
 * 
 * Adjustment to difficulty works currently as follows: 
 * 
 * Every increase in difficulty slightly decreases the allowed time until a minimum value is reached.
 * 
 * Until difficulty 4 the positions of the cities do not change.
 * Difficulty 1: map with borders, city alternatives are visible on map
 * Difficulty 2: map with borders
 * Difficulty 3: map without borders
 * Difficulty 4: map with false borders
 * Difficulty 5: map with borders, flipped horizontally
 * Difficulty 6: map without borders, flipped vertically
 * Difficulty 7: map without borders, flipped horizontally and vertically
 * Difficulty 8: map without borders, flipped in random fashion
 * Difficulty 9: as above, with a small random rotation
 * (this is VERY difficult with horizontal flip + rotation - should that be excluded?)
 * Difficulties 10-> as above (time limit might still decrease)
 * 
 * 
 * Possible tweaks and ideas: 
 * - displaying false location alternatives
 * - display the logo of the organization
 * - display the haalari of the organization
 * - rotating map
 * - maps of different subjects (e.g. Helsinki for TKO-aly, place Kumpula)
 * - Place on a vertical line (how up north the city is)
 * - Similarly for horizontal line
 * - 
 * 
 */
public class DifficultyAdjuster : MonoBehaviour {
    public TimeProgress timer;
    // Used to determine the difficulty of the map sprite
    public SpriteManager spriteManager;
    public float shortestPossibleTime = 1f;
    public int steps = 10;
    private InformationDisplayer[] cityDisplayers;



    public void Initialize(int difficulty) {
        timer = FindObjectOfType<TimeProgress>();
        cityDisplayers = FindObjectsOfType<InformationDisplayer>();
        spriteManager = GameObject.Find("Finland").GetComponent<SpriteManager>();

        TuneLocations(difficulty);
        TuneTime(difficulty);
        TuneSprite(difficulty);
        TuneFlipping(difficulty);
        TuneRotation(difficulty);

    }


    private void TuneLocations(int difficulty) {
        if (difficulty <= 2 || difficulty == 5) {
            for (int i = 0; i < cityDisplayers.Length; i++) {
                cityDisplayers[i].DisplayOnMap();
            }
        }
    }

    private void TuneTime(int difficulty) {
        // aika laskee lineaarisesti: joka vaikeustason nousulla
        // 1 / (2 * askelia)  * | pisin - lyhin | vähemmän aikaa
        // siis kun vaikeustaso on 2 * askelia, on jäljellä pienin mahdollinen määrä aikaa
        // koska eri efektejä on melko paljon, tuntui reilulta että aikaa on melko pitkään melko paljon; 
        // jos testaaminen osoittaa että tämä puoleen hidastaminen on turhaa, muokataan kaavaa
        timer.seconds = timer.seconds - ( difficulty / (2 * steps)) * (timer.seconds - shortestPossibleTime);

        if (timer.seconds < shortestPossibleTime) {
            timer.seconds = shortestPossibleTime;
        }
    }

    private void TuneSprite(int difficulty) {

        if (difficulty == 3 || difficulty >= 6) {
            spriteManager.ChangeSprite(1);
        } else if (difficulty == 4) {
            spriteManager.ChangeSprite(2);
        }
    }

    private void TuneRotation(int difficulty) {
        if (difficulty >= 9) {
            float angle = Random.Range(-25, 30);
            spriteManager.Rotate(angle);
        }    
    }

    private void TuneFlipping(int difficulty) {
        if (difficulty == 5) {
            spriteManager.Flip(true, false);
        } else if (difficulty == 6) {
            spriteManager.Flip(true, false);
        } else if (difficulty == 7) {
            spriteManager.Flip(true, true);
        } else if (difficulty >= 8){
            int idx = Mathf.FloorToInt(Random.Range(0f, 4f));
            // the two probabilities are independent of one another and both 1/2, so every
            // of the four combinations should be equally probable
            bool hor = idx % 2 == 0;
            bool vert = idx < 2;
            spriteManager.Flip(hor, vert);
        }
    }

}
