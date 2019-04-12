using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DebugBetweenScreenController : MonoBehaviour
{
	private string nextGame;
	private Dropdown dropdown;
	private int difficulty;
	public Text prevScore;
	public Text addScore;
	public Text newScore;
	public Text livesAmount;
	public InputField difficultySelect;

	

    // Start is called before the first frame update
    void Start() {
		dropdown = FindObjectOfType<Dropdown>();
		this.nextGame = "Random";
		this.difficulty = DataController.GetDifficulty();

		prevScore.text = "" + (DataController.GetCurrentScore() - DataController.GetLastReceivedScore());
		addScore.text = "" + DataController.GetLastReceivedScore();
		newScore.text = "" + DataController.GetCurrentScore();
		livesAmount.text = "" + DataController.GetLives();
		difficultySelect.text = "" + difficulty;
	}

	public void EndDebugScreen() {
		GameManager.nextGame(nextGame);
		DataController.SetDifficulty(int.Parse(difficultySelect.text));
	}

	public void SetNextGame(string next) {
		this.nextGame = next;
	}

}
