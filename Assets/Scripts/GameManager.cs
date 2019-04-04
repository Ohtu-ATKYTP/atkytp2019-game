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

    private string currentScene;
    private string[] games = {"PlaceCity", "TurkuGame", "LogoHaalariin", "ElevatorGame", "ElevatorRescue" };
    private string[] otherScenesThanGames = {"DebugBetweenGameScreen", "SceneManagerScene", "MainMenu", "BetweenGameScreen", "Highscores", "Registration", "Settings"};

    private DataController dataController;
    private WebServiceScript webService;

    private DevCheats devCheats;

    private void Start () {
        this.dataController = FindObjectOfType<DataController> ();
        this.webService = FindObjectOfType<WebServiceScript> ();
        
        if(!PlayerPrefs.HasKey("registered")){
            this.currentScene = "Registration";
            PlayerPrefs.SetInt("registered", 0);
        } else {
            this.currentScene = "MainMenu";
        }

        SceneManager.LoadScene (this.currentScene, LoadSceneMode.Additive);

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
		    SceneManager.UnloadSceneAsync(this.currentScene);
        } catch (System.Exception e) {
            Debug.Log(e);
            Debug.Log("Tried to unload: " + this.currentScene);
            Debug.Log("Throwing the error...");
            throw e;
        }
		if (this.currentScene != null) {
            lastGame = this.currentScene;
        }
		if (dataController.GetDebugMode()) {
			this.currentScene = "DebugBetweenGameScreen";
		} else {
			this.currentScene = "BetweenGameScreen";
		}
        SceneManager.LoadScene(this.currentScene, LoadSceneMode.Additive);
	}
	
	
    public void nextGame () {
        if (dataController.GetLives () == 0) {
            endGame (dataController.GetCurrentScore ());
        } else if (dataController.GetNextGame() == "Random") {
            getRandomGame ();
        } else {
			this.currentScene = dataController.GetNextGame();
			SceneManager.LoadScene(this.currentScene, LoadSceneMode.Additive);
			devCheats.ConfigureForNewMinigame(); 
		}
    }

    private void getRandomGame() {
        this.currentScene = this.games[Random.Range(0, this.games.Length)];
        while (this.currentScene == lastGame) {
            this.currentScene = this.games[Random.Range(0, this.games.Length)];
        }
        SceneManager.LoadScene(this.currentScene, LoadSceneMode.Additive);
        devCheats.ConfigureForNewMinigame(); 

    }

    private async void endGame (int score) //When the game is lost -- hävisit pelin
    {
        devCheats.ConfigureForNonMinigame();
        await SceneManager.UnloadSceneAsync (this.currentScene);
        SceneManager.LoadScene ("MainMenu", LoadSceneMode.Additive);

        if (PlayerPrefs.GetInt ("highScore") < score) {
            PlayerPrefs.SetInt ("highScore", score);
            string id = PlayerPrefs.GetString ("_id");
            HighScore updated = await webService.UpdateHighscore (id, score);
        }
        resetGameVariables ();

    }

    private void resetGameVariables() {
        this.currentScene = "MainMenu";
        dataController.Init ();
    }

    private void prepareNextGame () {
        dataController.SetStatus (DataController.Status.WAIT);
        SceneManager.UnloadSceneAsync (this.currentScene);
        nextGame ();
    }

    public string[] getGames() {
        return this.games;
    }

    public string[] getAllScenes() {
        return this.games.Concat(this.otherScenesThanGames).ToArray();
    }
}