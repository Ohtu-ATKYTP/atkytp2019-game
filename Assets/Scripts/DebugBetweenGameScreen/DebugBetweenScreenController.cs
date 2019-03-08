using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DebugBetweenScreenController : MonoBehaviour
{
	private DataController dataController;
	public Text prevScore;
	public Text addScore;
	public Text newScore;
	public Text livesAmount;
    // Start is called before the first frame update
    void Start() {
        dataController = FindObjectOfType<DataController>();
		prevScore.text = "" + (dataController.GetCurrentScore() - dataController.GetLastReceivedScore());
		addScore.text = "" + dataController.GetLastReceivedScore();
		newScore.text = "" + dataController.GetCurrentScore();
		livesAmount.text = "" + dataController.GetLives();
    }

	public void EndDebugScreen() {
		dataController.DebugBetweenScreenEnd();
	}
}
