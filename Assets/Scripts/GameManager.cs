using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {
    public int gamesStartIndex;
    public int gamesEndIndex;
    public Scene endGameScene;
	public float betweenGameWaitTime;

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
        if (dataController.GetReadyStatus()) {
            prepareNextGame();
        } else if (!dataController.GetBetweenGameShown() && dataController.GetRoundEndStatus()) {
			ExecuteBetweenGameScene();
		}
		
    }

    public void nextGame(bool win) {
        if (!win) dataController.TakeLife();

        if (dataController.GetLives() == 0) {
            endGame();
        } else {
            getRandomGame();
        }

    }

	private void ExecuteBetweenGameScene() {
		SceneManager.UnloadSceneAsync(this.game);
		dataController.SetBetweenGameShown(true);
		this.game = "BetweenGameScreen";
		SceneManager.LoadScene(this.game, LoadSceneMode.Additive);
	}

    private void getRandomGame() {
        if (game != null) {
            lastGame = game;
        }
        game = this.scenes[Random.Range(0, this.scenes.Length)];
        while (game == lastGame) {
            game = this.scenes[Random.Range(0, this.scenes.Length)];
        }
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
        this.game = "MainMenu";
        dataController.Init();
    }

    private void prepareNextGame() {
		dataController.SetReadyStatus(false);
        dataController.SetRoundEndStatus(false);
		dataController.SetBetweenGameShown(false);
        SceneManager.UnloadSceneAsync(this.game);
        nextGame(dataController.GetWinStatus());
    }
}
