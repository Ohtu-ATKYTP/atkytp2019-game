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
    }

    void Update() {
        if (hitboxes.Active() > Mathf.Min(40, difficulty) && !gameOver) {
            Win();
        }
    }

	public void Lose() {
		gameOver = true;
		StartCoroutine(RunLoseAnimation());
	}

    IEnumerator RunLoseAnimation() {
        flood.StartAnimation();
        yield return new WaitForSecondsRealtime(4);
        dataController.MinigameEnd(false, 0);
    }

	private void Win() {
		this.gameOver = true;
		TimeProgress timerScript = FindObjectOfType<TimeProgress>();
		timerScript.StopTimerProgression();
        StartCoroutine(RunWinAnimation());
	}

    IEnumerator RunWinAnimation() {
        turkuAnimation.StartAnimation();
        yield return new WaitForSecondsRealtime(3);
        dataController.MinigameEnd(true, 10);
    }
}
