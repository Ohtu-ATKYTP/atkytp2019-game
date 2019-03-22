using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DebugBetweenScreenController : MonoBehaviour
{
	private string nextGame;
	private DataController dataController;
	private Dropdown dropdown;
	private int difficulty;
	public Text prevScore;
	public Text addScore;
	public Text newScore;
	public Text livesAmount;
	public InputField difficultySelect;

    // Start is called before the first frame update
    void Start() {
        dataController = FindObjectOfType<DataController>();
		dropdown = FindObjectOfType<Dropdown>();
		this.nextGame = "Random";
		this.difficulty = dataController.GetDifficulty();

		prevScore.text = "" + (dataController.GetCurrentScore() - dataController.GetLastReceivedScore());
		addScore.text = "" + dataController.GetLastReceivedScore();
		newScore.text = "" + dataController.GetCurrentScore();
		livesAmount.text = "" + dataController.GetLives();
		difficultySelect.text = "" + difficulty;
	}

	public void EndDebugScreen() {
		dataController.SetNextGame(nextGame);
		dataController.SetDifficulty(int.Parse(difficultySelect.text));
		dataController.DebugBetweenScreenEnd();
	}

	public void SetNextGame(string next) {
		this.nextGame = next;
	}
}
