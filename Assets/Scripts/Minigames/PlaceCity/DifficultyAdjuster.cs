using System.Collections;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;



/**
 * 
 * Adjustment to difficulty works currently as follows: 
 * 
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
 * 
 * More time is given for the following difficulties
 * Effects of difficulty 9 also apply
 * 
 * Difficulty 10: Map moves along x or y axis
 * Difficulty 11: Map moves along a simple axis
 * Difficulty 12: Map moves along an axis
 * Difficulty 13: Map rotates around a point
 * Difficulty 14: Map rotates around its center
 * Difficulties 15-> Some rotation / panning 
 * 
 * 
 * Possible tweaks and ideas: 
 * - displaying false location alternatives
 * - don't show the name of the organization, only logo
 * - maps of different subjects (e.g. Helsinki for TKO-aly, place Kumpula)
 * - Place on a vertical line (how up north the city is)
 * - Similarly for horizontal line
 * 
 */
public class DifficultyAdjuster : MonoBehaviour {
    public TimeProgress timer;
    public SpriteManager spriteManager;
    public float shortestPossibleTime = 1f;
    public float shortestPossibleTimeWithMapMovement = 7f;
    public int steps = 10;
    private InformationDisplayer[] cityDisplayers;
    private CameraMotionController cameraController;
    private GameObject gamePane;
    private System.Random random = new System.Random();
    private GameObject gyroInstructions; 


    public void Initialize(int difficulty) {
        timer = FindObjectOfType<TimeProgress>();
        cityDisplayers = FindObjectsOfType<InformationDisplayer>();
        spriteManager = GameObject.Find("Finland").GetComponent<SpriteManager>();
        cameraController = Camera.main.GetComponent<CameraMotionController>();
        gamePane = GameObject.FindGameObjectWithTag("GamePane");
        random = new System.Random();

        TuneLocations(difficulty);
        TuneTime(difficulty);
        TuneSprite(difficulty);
        TuneFlipping(difficulty);

       // Tuning the positions of the cities is unfinished(?)
       // TuneFinlandRotation(difficulty);

        if (difficulty > 9) {
            gyroInstructions = GameObject.Find("GyroInstructionsPane");
            gyroInstructions.GetComponent<Canvas>().enabled = true; 
            gyroInstructions.GetComponentsInChildren<SpriteRenderer>().ToList().ForEach(sr => sr.enabled = true);
            PlaceCityManager mgr = FindObjectOfType<PlaceCityManager>();
            gyroInstructions.GetComponent<FadingInstructor>().Fade(1f, 2f, mgr.initialInstructionDuration + mgr.initialInstructionFadeDuration); 
            if (random.Next(0, 2) == 0) {
                TunePaneRotationInXYPlane(difficulty);
            } else {
                TunePanePanning(difficulty);
            }
        }

    }


    private void TuneLocations(int difficulty) {
        if (difficulty <= 2 || difficulty == 5) {
            for (int i = 0; i < cityDisplayers.Length; i++) {
                cityDisplayers[i].DisplayOnMap();
            }
        }
    }

    private void TuneTime(int difficulty) {
        if (difficulty > 9) {
            timer.SetTime(  20f - ((difficulty - 9) / 2) );
            if (timer.seconds < shortestPossibleTimeWithMapMovement) {
               timer.SetTime(  shortestPossibleTimeWithMapMovement );
            }
        } else {
            timer.SetTime( timer.seconds - (difficulty / (2 * steps)) * (timer.seconds - shortestPossibleTime) );
            if (timer.seconds < shortestPossibleTime) {
                timer.SetTime(shortestPossibleTime);
            }
        }

    }

    private void TuneSprite(int difficulty) {

        if (difficulty == 3 || difficulty >= 6) {
            spriteManager.ChangeSprite(1);
        } else if (difficulty == 4) {
            spriteManager.ChangeSprite(2);
        }
    }

    private void TuneFinlandRotation(int difficulty) {
        if (difficulty >= 9) {
            if (random.NextDouble() < .33f) {
                float angle = random.Next(-25, 30);
                spriteManager.Rotate(angle);
            }
        }
    }

    private void TuneFlipping(int difficulty) {
        if (difficulty == 5) {
            spriteManager.Flip(true, false);
        } else if (difficulty == 6) {
            spriteManager.Flip(true, false);
        } else if (difficulty == 7) {
            spriteManager.Flip(true, true);
        } else if (difficulty >= 8) {
            int idx = random.Next(0, 4);
            // the two probabilities are independent of one another and both 1/2, so every
            // of the four combinations should be equally probable
            bool hor = idx % 2 == 0;
            bool vert = idx < 2;
            spriteManager.Flip(hor, vert);
        }
    }

    private void TunePanePanning(int difficulty) {

        Vector2 movementDirection;
        if (difficulty <= 11) {
            movementDirection = random.NextDouble() < 0.5f ? Vector2.left : Vector2.right;
        } else if (difficulty < 13) {
            movementDirection = random.NextDouble() < 0.5f ? Vector2.down : Vector2.up;
        } else if (difficulty < 14) {
            Vector2[] easyDirections = new[] { Vector2.one, -1 * Vector2.one, new Vector2(1, -1), new Vector2(-1, 2) };
            movementDirection = easyDirections[random.Next(0, easyDirections.Length)];
        } else {
            movementDirection = Vector2.ClampMagnitude(new Vector2(random.Next(-1000, 1001), random.Next(-1000, 1001)), 1f);
        }

        gamePane.GetComponent<GamePanePanner>().Initialize(movementDirection, panningLengthInSecs: .3f * timer.seconds);
        cameraController.Initialize(new Dictionary<string, Vector2> { { "panning", movementDirection } });
    }

    private void TunePaneRotationInXYPlane(int difficulty) {
        bool clockWise = random.NextDouble() < .5f;
        Vector2 centerPoint = Vector2.zero;
        if (difficulty <= 11) {
            centerPoint = new Vector2(0, (random.NextDouble() < .5f ? -1 : 1) * 50);
        } else if (difficulty < 15) {
            float y = random.Next(-100, 100);
            centerPoint = new Vector2(0, y);
        } else if (difficulty < 17) {
            float x = random.Next(-100, 100);
            centerPoint = new Vector2(x, 0);
        } else {
            centerPoint = new Vector2(random.Next(-100, 100), random.Next(-100, 100));
        } 

        if (centerPoint.sqrMagnitude < 5) {
            centerPoint = Vector2.zero;
            gyroInstructions.GetComponentInChildren<Text>().text = "Käännä puhelin asentoon";
        }

        gamePane.GetComponent<GamePaneRotator>().Initialize(centerPoint: centerPoint, clockWise: clockWise, rotationLengthInSecs: .3f * timer.seconds);
        cameraController.Initialize(new Dictionary<string, Vector2> { { "rotation", centerPoint } });

    }

}
