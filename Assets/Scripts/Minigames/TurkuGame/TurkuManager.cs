using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TurkuManager : MonoBehaviour, IMinigameEnder {
    public HitboxManager hitboxes;
    public FloodAnimation flood;
    private DataController dataController;
    public int maxLineLength=1000;
    public int difficulty;
    private bool gameOver = false;
    public TurkuAnimation turkuAnimation;
    bool lineUsedUp = false;

    void Start() {
        dataController = FindObjectOfType<DataController>();
    }

    void Update() {
        if (hitboxes.Active() > Mathf.Min(hitboxes.HitboxCount()*0.95f, difficulty+(6*dataController.GetDifficulty())) && !gameOver && !lineUsedUp) {
            WinMinigame();
        }
    }


    public void WinMinigame() {
        this.gameOver = true;
        FindObjectOfType<TimeProgress>().StopTimerProgression();
        StartCoroutine(RunWinAnimation());
    }

    public void LineUsedUp(){
        lineUsedUp = true;
    }

    public bool GetLineUsedUp(){
        return lineUsedUp;
    }

    public int GetMaxLineLength(){
        return maxLineLength;
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
