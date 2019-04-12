using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;
using System.IO;                                                        // The System.IO namespace contains functions related to loading and saving files

public static class DataController
{
	//The MAIN_MENU flag is as of yet unused, can be used in a future implementation

	private static string nextGame;
	private static bool debug;
	private static readonly int DIFF_INCREASE_INTERVAL = 3;
    private static int currentScore = 0;
    private static bool winStatus;
    private static int lives = 3;
    private static readonly int MAX_LIVES = 3;
	private static int lastReceivedScore = 0;
	private static int roundsCompleted = 0;
	private static int difficulty = 1;
	private static string[] games;

	private static Dictionary<string, float> gameParameters = new Dictionary<string, float>();

    public static void Init() {
        currentScore = 0;
        winStatus = true;
        lives = MAX_LIVES;
		lastReceivedScore = 0;
		roundsCompleted = 0;
		debug = false;
		nextGame = "Random";
		difficulty = 1;

		gameParameters = new Dictionary<string, float>(); 
    }

	//Difficulty range is 1 and upwards
	public static int GetDifficulty() {
		return difficulty;
	}

	public static void SetDifficulty(int newDiff) {
		difficulty = newDiff;
	}

    public static void UpdateDifficulty() {
        difficulty = roundsCompleted / DIFF_INCREASE_INTERVAL + 1;
    }

    public static void incrementRoundsCompleted() {
        roundsCompleted++;
    }

    public static void TakeLife() {
        lives--;
    }

    public static int GetLives() {
        return lives;
    }

    public static bool GetWinStatus() {
        return winStatus;
    }

    public static void SetWinStatus(bool win) {
        winStatus = win;
    }

    public static int GetCurrentScore() {
        return currentScore;
    }

	public static void AddCurrentScore(int score) {
		lastReceivedScore = score;
        currentScore += score;
    }

	public static int GetLastReceivedScore() {
		return lastReceivedScore;
	}

	public static void SetDebugMode(bool debugIsOn) {
		debug = debugIsOn;
	}

	public static bool GetDebugMode() {
		return debug;
	}

	public static string[] GetGames() {
		return games;
	}

	public static string GetNextGame() {
		return nextGame;
	}

	public static void SetNextGame(string next) {
		nextGame = next;
	}

	//Used to adjust Game Parameters from debug play Screen
	public static void setGameParameter(string key, float value) {
		if(gameParameters.ContainsKey(key)){
			gameParameters[key] = value;
		}else{
			gameParameters.Add(key, value);
		}
	}

	public static float getGameParameter(string key) {
		return gameParameters[key];
	}

	public static bool hasGameParameter(string key) {
		return gameParameters.ContainsKey(key);
	}
}
