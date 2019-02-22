﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {
    public int gamesStartIndex;
    public int gamesEndIndex;
    public Scene endGameScene;

    private string lastGame;
    private string game;
    private string[] scenes = { "FirstGame", "SecondGame", "PlaceCity", "TurkuGame" };
    private string mainmenuScreen = "MainMenu";
    private string endGameScreen = "MainMenu";
    private DataController dataController;
    private HighScoreManager HSManager;
    
    private void Start() {
        this.dataController = FindObjectOfType<DataController>();
        this.HSManager = FindObjectOfType<HighScoreManager>();
        
        SceneManager.LoadScene(this.mainmenuScreen, LoadSceneMode.Additive);
        this.game = this.mainmenuScreen;
        this.lastGame = "";
    }

    private void Update() {
        if (dataController.GetStatus() == DataController.Status.MINIGAME) {
			Debug.Log("starting new minigame");
            prepareNextGame();
        } else if (dataController.GetStatus() == DataController.Status.BETWEEN) {
			//Execute between screen scene
			Debug.Log("going to between screen");
			ExecuteBetweenScreen();
		} else if (dataController.GetStatus() == DataController.Status.MAIN_MENU) {
			//Go to main menu
		}
    }

	private void ExecuteBetweenScreen() {
		dataController.SetStatus(DataController.Status.WAIT);
		SceneManager.UnloadSceneAsync(this.game);
		this.game = "BetweenGameScreen";
        SceneManager.LoadScene(this.game, LoadSceneMode.Additive);
	}

    public void nextGame(bool win) {
        if (!win) dataController.TakeLife();

        if (dataController.GetLives() == 0) {
            endGame();
        } else {
            getRandomGame();
        }

        // kutsutaan datacontroller
    }

    private void getRandomGame() {
        if (game != null) {
            lastGame = game;
        }
        game = this.scenes[Random.Range(0, this.scenes.Length)];
        while (game == lastGame) {
            game = this.scenes[Random.Range(0, this.scenes.Length)];
        }
		Debug.Log("Loading scene " + this.game);
        SceneManager.LoadScene(game, LoadSceneMode.Additive);
    }

    private void endGame() //When the game is lost -- hävisit pelin
    {
        SceneManager.UnloadSceneAsync(this.game);
        SceneManager.LoadScene(endGameScreen, LoadSceneMode.Additive);
        if (PlayerPrefs.GetInt("highScore") < dataController.GetCurrentScore()) {
            PlayerPrefs.SetInt("highScore", dataController.GetCurrentScore());
            PlayerPrefs.SetInt("syncedHS", 0);
            HSManager.StartSync();
        
        }
        resetGameVariables();

    }

    private void resetGameVariables() {
        Debug.Log("Päästin resetGameVariables");
        this.game = "MainMenu";
        dataController.Init();
    }

    private void prepareNextGame() {
		dataController.SetStatus(DataController.Status.WAIT);
        SceneManager.UnloadSceneAsync(this.game);
        nextGame(dataController.GetWinStatus());
    }
}
