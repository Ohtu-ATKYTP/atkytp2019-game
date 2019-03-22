using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Linq;

public class GameManager : MonoBehaviour {
    public int gamesStartIndex;
    public int gamesEndIndex;
    public Scene endGameScene;
    private string lastGame;
    private string game;
    private string[] games = {"PlaceCity", "TurkuGame", "LogoHaalariin"};
    private string[] otherScenesThanGames = {"DebugBetweenGameScreen", "SceneManagerScene", "MainMenu", "BetweenGameScreen"};
    private string mainmenuScreen = "MainMenu";
    private string endGameScreen = "MainMenu";
    private DataController dataController;
    private WebServiceScript webService;

    private DevCheats devCheats;

    private void Start () {
        this.dataController = FindObjectOfType<DataController> ();
        this.webService = FindObjectOfType<WebServiceScript> ();

        SceneManager.LoadScene (this.mainmenuScreen, LoadSceneMode.Additive);
        this.game = this.mainmenuScreen;
        this.lastGame = "";
		dataController.SetGames(games);
        devCheats = GetComponent<DevCheats>();
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
        try {
		    SceneManager.UnloadSceneAsync(this.game);
        } catch (System.Exception e) {
            Debug.Log(e);
            Debug.Log("Tried to unload: " + this.game);
            Debug.Log("Throwing the error...");
            throw e;
        }
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
	
	
    public void nextGame () {
        if (dataController.GetLives () == 0) {
            endGame (dataController.GetCurrentScore ());
        } else if (dataController.GetNextGame() == "Random") {
            getRandomGame ();
        } else {
			this.game = dataController.GetNextGame();
			SceneManager.LoadScene(this.game, LoadSceneMode.Additive);
			devCheats.ConfigureForNewMinigame(); 
		}
    }

    private void getRandomGame() {
        game = this.games[Random.Range(0, this.games.Length)];
        while (game == lastGame) {
            game = this.games[Random.Range(0, this.games.Length)];
        }
        SceneManager.LoadScene(this.game, LoadSceneMode.Additive);
        devCheats.ConfigureForNewMinigame(); 

 }

    private async void endGame (int score) //When the game is lost -- hävisit pelin
    {
        devCheats.ConfigureForNonMinigame();
        await SceneManager.UnloadSceneAsync (this.game);
        SceneManager.LoadScene (endGameScreen, LoadSceneMode.Additive);

        if (PlayerPrefs.GetInt ("highScore") < score) {
            PlayerPrefs.SetInt ("highScore", score);
            string id = PlayerPrefs.GetString ("_id");
            HighScore updated = await webService.UpdateHighscore (id, score);

            if (updated == null) {
                PlayerPrefs.SetInt ("syncedHS", 0);
            }

            HighScore highscore = await webService.GetOne (id);

            if (highscore != null) {
                PlayerPrefs.SetInt ("rank", highscore.rank);
            }

        }
        resetGameVariables ();

    }

    private void resetGameVariables() {
        this.game = "MainMenu";
        dataController.Init ();
    }

    private void prepareNextGame () {
        dataController.SetStatus (DataController.Status.WAIT);
        SceneManager.UnloadSceneAsync (this.game);
        nextGame ();
    }

    public string[] getGames() {
        return this.games;
    }

    public string[] getAllScenes() {
        return this.games.Concat(this.otherScenesThanGames).ToArray();
    }
}