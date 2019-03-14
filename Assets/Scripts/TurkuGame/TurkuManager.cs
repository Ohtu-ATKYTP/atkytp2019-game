using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TurkuManager : MonoBehaviour, IMinigameEnder {
    public HitboxManager hitboxes;
    public FloodAnimation flood;
    private DataController dataController;
    public int difficulty;
    private bool gameOver = false;
    public TurkuAnimation turkuAnimation;

    void Start() {
        dataController = FindObjectOfType<DataController>();
    }

    void Update() {
        if (hitboxes.Active() > Mathf.Min(40, difficulty) && !gameOver) {
            WinMinigame();
        }
    }


    public void WinMinigame() {
        this.gameOver = true;
        FindObjectOfType<TimeProgress>().StopTimerProgression();
        StartCoroutine(RunWinAnimation());
    }


    public void LoseMinigame() {
        gameOver = true;
        StartCoroutine(RunLoseAnimation());
        FindObjectOfType<TimeProgress>().StopTimerProgression();
    }

    IEnumerator RunLoseAnimation() {
        flood.StartAnimation();
        yield return new WaitForSecondsRealtime(4);
        dataController.MinigameEnd(false, 0);
    }

    IEnumerator RunWinAnimation() {
        turkuAnimation.StartAnimation();
        yield return new WaitForSecondsRealtime(3);
        dataController.MinigameEnd(true, 10);
    }


}
