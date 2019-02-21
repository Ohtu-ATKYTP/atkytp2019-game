using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TurkuManager : MonoBehaviour {
    public HitboxManager hitboxes;
    public FloodAnimation flood;
    private DataController dataController;
    public int difficulty;
    private bool gameOver = false;
    public TurkuAnimation turkuAnimation;

    void Start() {
        dataController = FindObjectOfType<DataController>();
        StartCoroutine(Lose());
    }

    void Update() {
        if (hitboxes.Active() > Mathf.Min(40, difficulty) && !gameOver) {
            this.gameOver = true;
            this.Win();
        }
    }

    public void EndMinigame(bool win) {
        dataController.SetRoundEndStatus(true);
        dataController.SetWinStatus(win);
        dataController.AddCurrentScore(10);
    }

    IEnumerator Lose() {
        yield return new WaitForSecondsRealtime(10);
        flood.StartAnimation();
        yield return new WaitForSecondsRealtime(4);
        EndMinigame(false);
    }

    private void Win() {
        StartCoroutine(EndGame());
        Debug.Log("VOITIT PELIN!");
    }

    IEnumerator EndGame() {
        turkuAnimation.StartAnimation();
        yield return new WaitForSecondsRealtime(3);
        EndMinigame(true);
    }
}
