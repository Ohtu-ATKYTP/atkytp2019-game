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
        if (dataController.GetRoundEndStatus()) {
            prepareNextGame();

        }
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
        Debug.Log("Päästin resetGameVariables");
        this.game = "MainMenu";
        dataController.Init();
    }

    private void prepareNextGame() {
        dataController.SetRoundEndStatus(false);
        SceneManager.UnloadSceneAsync(this.game);
        nextGame(dataController.GetWinStatus());
    }
}
