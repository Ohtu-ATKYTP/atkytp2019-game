using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public int gamesStartIndex;
    public int gamesEndIndex;
    public Scene endGameScene;
    private int lives;
    private string lastGame;
    private string game;
    private string[] scenes = {"FirstGame", "SecondGame"};
     private DataController dataController;

    private void Start() {
        dataController = FindObjectOfType<DataController>();
        startGame();    
    }

    private void Update() {
        if (dataController.GetRoundEndStatus()) {
            dataController.SetRoundEndStatus(false);
            nextGame(dataController.GetWinStatus());
            SceneManager.UnloadSceneAsync(this.game);
        }
    }

    public void startGame()
    {
        lastGame = "";
        lives = 3;
        nextGame(true);
    }

    public void nextGame(bool win)
    {
        if (!win) lives--;
        if (lives == 0)
        {
            endGame();
        }
        else
        {
            getRandomGame();
        }
    }

    private void getRandomGame()
    {
        if (game != null) {
            lastGame = game;
        }
        game = this.scenes[Random.Range(0, this.scenes.Length)];
        while(game == lastGame)
        {
            game = this.scenes[Random.Range(0, this.scenes.Length)];
        }
        SceneManager.LoadScene(game, LoadSceneMode.Additive);
    }

    private void endGame()
    {

        Debug.Log("HÄVISIT PELIN!");
    }
}
