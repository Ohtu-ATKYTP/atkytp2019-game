﻿using UnityEngine;

public abstract class MinigameLogic : MonoBehaviour, IMinigameEnder {
    [SerializeField]
    protected int pointsForWinning = 10;
    [SerializeField]
    protected int testingDifficulty = 1;
    protected TimeProgress timebar;

    protected virtual void Start() {
        timebar = FindObjectOfType<TimeProgress>();
        if (timebar.TimerReadyMethods.GetPersistentEventCount() == 0) {
            timebar.TimerReadyMethods.AddListener(OnTimerEnd);
        }
        ConfigureDifficulty(DataController.GetDifficulty() != null ?
                             DataController.GetDifficulty()
                             : testingDifficulty);
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
