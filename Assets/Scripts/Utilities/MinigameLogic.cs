using UnityEngine;
using UnityEngine.Events;

public abstract class MinigameLogic : MonoBehaviour, IMinigameEnder {
    private DataController dataController;
    [SerializeField]
    private int pointsForWinning = 10;
    [SerializeField]
    private int testingDifficulty = 1;
    TimeProgress timebar;




    protected virtual void Start() {
        dataController = FindObjectOfType<DataController>();
        timebar = FindObjectOfType<TimeProgress>();
        if(timebar.TimerReadyMethods.GetPersistentEventCount() == 0)
        {
            timebar.TimerReadyMethods.AddListener(OnTimerEnd);
        }
        ConfigureDifficulty(dataController != null ?
                             dataController.GetDifficulty()
                             : testingDifficulty);
    }


    protected void EndMinigame(bool won) {
        FindObjectOfType<TimeProgress>().StopTimerProgression();

        DisplayEndingActions(won);

        if (!dataController)
        {
            Debug.Log("Minigame would end now");
        } else {
            dataController.MinigameEnd(won,
                    won ? pointsForWinning : 0
                );
        }
    }


    public virtual void OnTimerEnd()
    {
        LoseMinigame();
    }

    protected abstract void DisplayEndingActions(bool won);


    protected abstract void ConfigureDifficulty(int difficulty);

    #region Implements IMinigameEnder
    public void LoseMinigame()
    {
        EndMinigame(false);
    }

    public void WinMinigame()
    {
        EndMinigame(true);
    }
    #endregion

}
