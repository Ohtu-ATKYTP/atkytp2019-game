using System.Threading.Tasks;
using UnityEngine;

public class MinigameLogic : MonoBehaviour {
    private DataController dataController;
    [SerializeField]
    [Tooltip("Higher values mean more difficult minigame. Will be requested from the data manager in the game, but set here if you want to test only this scene.")]
    private int difficulty = 1;
    [SerializeField]
    private int pointsForWinning = 10;
    [Tooltip("The delay between the time of losing/winning, and the time of unloading the minigame scene (in seconds)")]
    public int delayAfterMinigameEndsInSeconds = 2;



    void Start() {
        dataController = FindObjectOfType<DataController>();
        difficulty = dataController.GetDifficulty();
    }


    public void OnTimerEnd() {
        // If something additional to losing the game needs to happen when the time runs out, 
        // those actions should be specified before the call to EndMinigame

        // Placeholder: spawn a sphere at the origin of the world space
        //GameObject go = GameObject.CreatePrimitive(PrimitiveType.Sphere);

        EndMinigame(false);
    }



    // Implement the methods for keeping track of the status of the goals and player progress
    // Or in another script that calls the methods of this one



    public async void EndMinigame(bool won) {
        // Ensuring that the timer is always stopped when the minigame ends
        FindObjectOfType<TimeProgress>().StopTimerProgression();

        // Here: actions that are made in the minigame once the result is certain, e.g. text of success, starting ending animation 
        Debug.Log("The scene adapts to ending");

        await Task.Delay(1000 * delayAfterMinigameEndsInSeconds);

        Debug.Log("The scene is ready to be changed.");
        dataController.MinigameEnd(won,
                won ? pointsForWinning : 0
            );
    }
}
