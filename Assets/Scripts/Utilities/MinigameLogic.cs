using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinigameLogic : MonoBehaviour {
    private DataController dataController;
    [SerializeField]
    [Tooltip("Higher values mean more difficult minigame")]
    private int difficulty;
    [SerializeField]
    private int pointsForWinning = 10;

    void Start() {
        dataController = FindObjectOfType<DataController>();
        // dataController.GetDifficulty();
        difficulty = -1;
    }


    public void OnTimerEnd() {
        // If something additional to losing the game needs to happen when the time runs out, 
        // those actions should be specified before the call to EndMinigame

        // Placeholder: spawn a sphere at the origin of the world space
        //GameObject sphereOfWastedTime = GameObject.CreatePrimitive(PrimitiveType.Sphere);



        // Time running out will result in a lost game
        EndMinigame(false);
    }


    public void EndMinigame(bool won) {
        dataController.MinigameEnd(won,
                won ? pointsForWinning : 0
            );

    }
}
