using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BetweenScreenController : MonoBehaviour
{
	private DataController dataController;
	private float startTime;
	public Text prevScore;
	public Text addScore;
	public Text newScore;
	public Text livesAmount;
	private bool active;

    // Start is called before the first frame update
    void Start() {
		active = true;
		dataController = FindObjectOfType<DataController>();
        startTime = Time.time;
		prevScore.text = "" + (dataController.GetCurrentScore() - dataController.GetLastReceivedScore());
		addScore.text = "" + dataController.GetLastReceivedScore();
		newScore.text = "" + dataController.GetCurrentScore();
		livesAmount.text = "" + dataController.GetLives();
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time - startTime > 3 && active) {
			active = false;
			dataController.BetweenScreenEnd();
		}
    }
}
