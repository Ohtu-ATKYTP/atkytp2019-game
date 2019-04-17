using UnityEngine;

public abstract class MinigameLogic : MonoBehaviour, IMinigameEnder
{
    [SerializeField]
    protected int pointsForWinning = 10;
    protected TimeProgress timebar;

    protected virtual void Start() {
        timebar = FindObjectOfType<TimeProgress>();
        timebar.TimerReadyMethods.RemoveAllListeners();
        timebar.TimerReadyMethods.AddListener(OnTimerEnd);

        ConfigureDifficulty(DataController.GetDifficulty());
    }


    protected void EndMinigame(bool won) {
        FindObjectOfType<TimeProgress>().StopTimerProgression();

        DisplayEndingActions(won);
        GameManager.endMinigame(won,
                won ? pointsForWinning : 0
        );
    }


    public virtual void OnTimerEnd() {
        LoseMinigame();
    }

    protected abstract void DisplayEndingActions(bool won);


    protected abstract void ConfigureDifficulty(int difficulty);

    #region Implements IMinigameEnder
    public void LoseMinigame() {
        EndMinigame(false);
    }

    public void WinMinigame() {
        EndMinigame(true);
    }
    #endregion

}
