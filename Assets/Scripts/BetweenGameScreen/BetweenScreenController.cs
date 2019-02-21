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

    // Start is called before the first frame update
    void Start() {
		dataController = FindObjectOfType<DataController>();
        startTime = Time.time;
		Debug.Log("lives" + dataController.GetLives());
		prevScore.text = "" + (dataController.GetCurrentScore() - dataController.GetLastReceivedScore());
		addScore.text = "" + dataController.GetLastReceivedScore();
		newScore.text = "" + dataController.GetCurrentScore();
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time - startTime > 3) {
			dataController.BetweenScreenEnd();
		}
		
    }
}
