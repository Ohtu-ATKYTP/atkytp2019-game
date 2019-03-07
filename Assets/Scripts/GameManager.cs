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
    private string[] scenes = { "FirstGame", "SecondGame", "PlaceCity", "TurkuGame", "LogoHaalariin" };
    private string mainmenuScreen = "MainMenu";
    private string endGameScreen = "MainMenu";
    private DataController dataController;
    private WebServiceScript webService;

    private void Start () {
        this.dataController = FindObjectOfType<DataController> ();
        this.webService = FindObjectOfType<WebServiceScript> ();

        SceneManager.LoadScene (this.mainmenuScreen, LoadSceneMode.Additive);
        this.game = this.mainmenuScreen;
        this.lastGame = "";
    }

    private void Update () {
        if (dataController.GetStatus () == DataController.Status.MINIGAME) {
            dataController.SetStatus (DataController.Status.WAIT);
            prepareNextGame ();
        } else if (dataController.GetStatus () == DataController.Status.BETWEEN) {
            //Execute between screen scene
            Debug.Log ("going to between screen");
            ExecuteBetweenScreen ();
        } else if (dataController.GetStatus () == DataController.Status.MAIN_MENU) {
            //Go to main menu (future implementation?)
        }
    }

    private void ExecuteBetweenScreen () {
        dataController.SetStatus (DataController.Status.WAIT);
        SceneManager.UnloadSceneAsync (this.game);
        this.game = "BetweenGameScreen";
        SceneManager.LoadScene (this.game, LoadSceneMode.Additive);
    }

    public void nextGame () {
        if (dataController.GetLives () == 0) {
            endGame (dataController.GetCurrentScore ());
        } else {
            getRandomGame ();
        }
    }

    private void getRandomGame () {
        if (game != null) {
            lastGame = game;
        }
        game = this.scenes[Random.Range (0, this.scenes.Length)];
        while (game == lastGame) {
            game = this.scenes[Random.Range (0, this.scenes.Length)];
        }
        Debug.Log ("Loading scene " + this.game + " with status " + dataController.GetStatus ());
        SceneManager.LoadScene (game, LoadSceneMode.Additive);
    }

    private async void endGame (int score) //When the game is lost -- hävisit pelin
    {
        SceneManager.UnloadSceneAsync (this.game);
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

    private void resetGameVariables () {
        Debug.Log ("Päästin resetGameVariables");
        this.game = "MainMenu";
        dataController.Init ();
    }

    private void prepareNextGame () {
        dataController.SetStatus (DataController.Status.WAIT);
        SceneManager.UnloadSceneAsync (this.game);
        nextGame ();
    }
}