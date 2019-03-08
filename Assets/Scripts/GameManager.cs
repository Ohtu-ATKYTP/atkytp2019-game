using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {
    public int gamesStartIndex;
    public int gamesEndIndex;
    public Scene endGameScene;

    private string lastGame;
    private string game;
    private string[] scenes = { "FirstGame", "PlaceCity", "TurkuGame", "LogoHaalariin"};
    private string mainmenuScreen = "MainMenu";
    private string endGameScreen = "MainMenu";
    private DataController dataController;
    private HighScoreManager HSManager;
    private RankManager rankManager;
    
    private void Start() {
        this.dataController = FindObjectOfType<DataController>();
        this.HSManager = FindObjectOfType<HighScoreManager>();
        this.rankManager = FindObjectOfType<RankManager>();

        
        SceneManager.LoadScene(this.mainmenuScreen, LoadSceneMode.Additive);
        this.game = this.mainmenuScreen;
        this.lastGame = "";
    }

    private void Update() {
        if (dataController.GetStatus() == DataController.Status.MINIGAME) {
			dataController.SetStatus(DataController.Status.WAIT);
            prepareNextGame();
        } else if (dataController.GetStatus() == DataController.Status.BETWEEN) {
			//Execute between screen scene
			ExecuteBetweenScreen();
		} else if (dataController.GetStatus() == DataController.Status.MAIN_MENU) {
			//Go to main menu (future implementation?)
		}
    }

	private void ExecuteBetweenScreen() {
		dataController.SetStatus(DataController.Status.WAIT);
		SceneManager.UnloadSceneAsync(this.game);
		if (game != null) {
            lastGame = game;
        }
		if (dataController.GetDebugMode()) {
			this.game = "DebugBetweenGameScreen";
		} else {
			this.game = "BetweenGameScreen";
		}
        SceneManager.LoadScene(this.game, LoadSceneMode.Additive);
	}

    public void nextGame(bool win) {
        if (dataController.GetLives() == 0) {
            endGame();
        } else {
            getRandomGame();
        }
    }

    private void getRandomGame() {
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

            //Jos halutaan, että ajantasaiset näkyvät jossain loppuruudussa niin synkkaysta
            //pitää odottaa. 
            HSManager.StartSync();
            //Nyt rankin haku odottaa eka 3 sek että highscore ehditään lähettää
            //Nämä voi muuttaa sillain että callback odottaa vahvistusta
            rankManager.AfterGameRank();
        
        }
        resetGameVariables();

    }

    private void resetGameVariables() {
        this.game = "MainMenu";
        dataController.Init();
    }

    private void prepareNextGame() {
		dataController.SetStatus(DataController.Status.WAIT);
        SceneManager.UnloadSceneAsync(this.game);
        nextGame(dataController.GetWinStatus());
    }
}