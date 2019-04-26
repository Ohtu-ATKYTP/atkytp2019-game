using System.Threading.Tasks;
using UnityEngine;

public class ElevatorRescueLogic : MonoBehaviour, IMinigameEnder {
    [SerializeField]
    [Tooltip("Higher values mean more difficult minigame. Will be requested from the data manager in the game, but set here if you want to test only this scene.")]
    private int difficulty = 1;
    [Tooltip("The delay between the time of losing/winning, and the time of unloading the minigame scene (in seconds)")]
    public int delayAfterMinigameEndsInSeconds = 2;

    private Vector3 originalGravity;
    [SerializeField]
    private Vector3 newGravity = new Vector3(0.0f, 3000.0f, 0.0f);

    public BoxCollider2D escapeTrigger;

    private bool gameIsOver = false;
    private ElevatorRescueSetup ers;

    void Start() {
        difficulty = DataController.GetDifficulty();
        ers = gameObject.GetComponent<ElevatorRescueSetup>();
        ers.Setup(difficulty);
        //setup the scene based on difficulty

        //Use custom gravity for scene
        originalGravity = Physics.gravity;
        Physics.gravity = newGravity;
    }


    public void OnTimerEnd() {
        // If something additional to losing the game needs to happen when the time runs out, 
        // those actions should be specified before the call to EndMinigame

        // Placeholder: spawn a sphere at the origin of the world space
        //GameObject go = GameObject.CreatePrimitive(PrimitiveType.Sphere);

        LoseMinigame();
    }



    // Implement the methods for keeping track of the status of the goals and player progress
    // Or in another script that calls the methods of this one
    public void FixedUpdate () {
        if (!gameIsOver) {
            WinCheck();
        }

    }

    private void WinCheck() {
        bool escaped = true;
        Collider2D[] hitColliders = Physics2D.OverlapBoxAll(escapeTrigger.transform.position, escapeTrigger.size * 4.5f, 0.0f);
        foreach (Collider2D col in hitColliders) {
            if (col.tag == "Escapee") {
                escaped = false;
            }
        }
        if (escaped) {
            gameIsOver = true;
            WinMinigame();
        }
    }

    public void OnDrawGizmos() {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(escapeTrigger.transform.position, escapeTrigger.size * 4.5f);
    }

    public async void EndMinigame(bool won) {
        // Ensuring that the timer is always stopped when the minigame ends
        FindObjectOfType<TimeProgress>().StopTimerProgression();

        // Here: actions that are made in the minigame once the result is certain, e.g. text of success, starting ending animation 
        Debug.Log("The scene adapts to ending");

        await Task.Delay(1000 * delayAfterMinigameEndsInSeconds);

        Debug.Log("The scene is ready to be changed.");
        GameManager.endMinigame(won);
    }

    public void LoseMinigame() {
        EndMinigame(false);
    }

    public void WinMinigame() {
        EndMinigame(true);
    }

    public bool GetGameOver() {
        return this.gameIsOver;
    }
}
