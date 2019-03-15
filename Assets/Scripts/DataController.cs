using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using System.IO;                                                        // The System.IO namespace contains functions related to loading and saving files

public class DataController : MonoBehaviour 
{
	//The MAIN_MENU flag is as of yet unused, can be used in a future implementation
	public enum Status {
		WAIT,
		MAIN_MENU,
		MINIGAME,
		BETWEEN
	}
	private string nextGame;
	private bool debug;
    private RoundData[] allRoundData;
	private readonly int DIFF_INCREASE_INTERVAL = 3;
    private int currentScore;
    private bool winStatus;
    private int lives;
    private readonly int MAX_LIVES = 3;
	private int lastReceivedScore;
	private Status status;
	private int roundsCompleted;
	private int difficulty;
	private string[] games;

    void Start()
    {
        DontDestroyOnLoad(gameObject);
        Init();
    }

    public void Init() {
        this.currentScore = 0;
        this.winStatus = true;
        this.lives = MAX_LIVES;
		this.status = Status.WAIT;
		this.lastReceivedScore = 0;
		this.roundsCompleted = 0;
		this.debug = false;
		this.nextGame = "Random";
		this.difficulty = 1;
    }

	//Difficulty range is 1 and upwards
	public int GetDifficulty() {
		return this.difficulty;
	}

	public void SetDifficulty(int newDiff) {
		this.difficulty = newDiff;
	}

    public void MinigameEnd(bool win, int score) {
        this.SetWinStatus(win);
		if (!win) this.lives--;
        this.AddCurrentScore(score);
		this.status = Status.BETWEEN;
		this.roundsCompleted++;
		this.difficulty = this.roundsCompleted / this.DIFF_INCREASE_INTERVAL + 1;
    }

	public void BetweenScreenEnd() {
		this.status = Status.MINIGAME;
	}

	public void DebugBetweenScreenEnd() {
		this.status = Status.MINIGAME;
	}

    public void TakeLife() {
        this.lives--;
    }

    public int GetLives() {
        return this.lives;
    }

    public void ResetLives() {
        this.lives = 3;
    }

    public bool GetWinStatus() {
        return this.winStatus;
    }

    public void ResetScore() {
        this.currentScore = 0;
    }

    public void SetWinStatus(bool win) {
        this.winStatus = win;
    }

    public int GetCurrentScore() {
        return this.currentScore;
    }

	public void AddCurrentScore(int score) {
		this.lastReceivedScore = score;
        this.currentScore += score;
    }

	public Status GetStatus() {
		return this.status;
	}

	public void SetStatus(Status newStatus) {
		this.status = newStatus;
	}

	public int GetLastReceivedScore() {
		return this.lastReceivedScore;
	}

	public void SetDebugMode(bool debugIsOn) {
		this.debug = debugIsOn;
	}

	public bool	GetDebugMode() {
		return this.debug;
	}

	public void SetGames(string[] gameScenes) {
		this.games = gameScenes;
	}

	public string[] GetGames() {
		return this.games;
	}

	public string GetNextGame() {
		return this.nextGame;
	}

	public void SetNextGame(string next) {
		this.nextGame = next;
	}
}
