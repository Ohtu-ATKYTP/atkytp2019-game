using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {
    public int gamesStartIndex;
    public int gamesEndIndex;
    public Scene endGameScene;
    private int lives;
    private string lastGame;
    private string game;
    private string[] scenes = { "FirstGame", "SecondGame" };
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
        this.lives = 3;
    }

    private void Update() {
        if (dataController.GetRoundEndStatus()) {
            prepareNextGame();

        }
    }

    public void nextGame(bool win) {
        if (!win) lives--;

        if (lives == 0) {
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
        this.lives = 3;
        dataController.ResetScore();
        dataController.SetWinStatus(true);
    }

    private void prepareNextGame() {
        dataController.SetRoundEndStatus(false);
        SceneManager.UnloadSceneAsync(this.game);
        nextGame(dataController.GetWinStatus());
    }
}
