using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public int gamesStartIndex;
    public int gamesEndIndex;
    public Scene endGameScene;
    private int score;
    private int lives;
    private int lastGame;

    public void startGame()
    {
        lastGame = -1;
        score = 0;
        lives = 3;
        nextGame(0, false);
    }

    public void nextGame(int gameScore, bool fail)
    {
        score += gameScore;
        if (fail) lives--;
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
        int game = Random.Range(gamesStartIndex, gamesEndIndex);
        while(game == lastGame)
        {
            game = Random.Range(gamesStartIndex, gamesEndIndex);
        }
        SceneManager.SetActiveScene(SceneManager.GetSceneByBuildIndex(game));
    }

    private void endGame()
    {
        Debug.Log(score);
        SceneManager.SetActiveScene(endGameScene);
    }
}
