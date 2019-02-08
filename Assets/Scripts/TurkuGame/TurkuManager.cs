using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TurkuManager : MonoBehaviour
{
    public FloodAnimation flood;
    public Text t;
    private DataController dataController;
    private void Start()
    {
        dataController = FindObjectOfType<DataController>();
        StartCoroutine(Lose());
    }
    // Start is called before the first frame update
    public void EndMinigame(bool win)
    {
        dataController.SetRoundEndStatus(true);
        dataController.SetWinStatus(win);
        dataController.AddCurrentScore(10);
    }

    IEnumerator Lose()
    {
        yield return new WaitForSecondsRealtime(10);
        t.enabled = false;
        flood.StartAnimation();
        yield return new WaitForSecondsRealtime(4);
        EndMinigame(false);
    }
}
