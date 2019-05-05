using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Linq;

public static class GameManager
{
    private static string currentGame;
    
    private static string[] games = {"PlaceCity", "TurkuGame", "LogoHaalariin", "ElevatorGame", "ElevatorRescue", "MetroGame", "Tamperelainen", "Jallupullo"};
    private static string betweenGameScreen = "BetweenGameScreen";
    private static string mainMenu = "MainMenu";

    public static void StartGame()
    {
        DataController.Init();
        NextGame();
    }

    public static void NextGame(string game = "Random")
    {
        if (game == "Random")
        {
            game = GetRandomGame();
        }
        SceneManager.LoadScene(game);
        currentGame = game;
    }

    public static void EndMinigame(bool win)
    {
        DataController.SetWinStatus(win);
        if (!win)
        {
            DataController.TakeLife();
        }
        // Base score = 10 if win, else 0
        int score = win ? 10 : 0;
        // Adding to score based on difficulty
        int difficultyAddition = DataController.GetDifficulty() / 2;
        score += difficultyAddition;
        // Adding to score random number between -4 and 4
        int randomAddition = Random.Range(-4, 5);
        score += randomAddition;

        DataController.AddCurrentScore(score);

        DataController.incrementRoundsCompleted();
        DataController.UpdateDifficulty();

        // Debug mode checker
        if (!DataController.GetDebugMode())
        {
            BetweenGame();
        }
        else
        {
            DebugBetweenGame();
        }
    }

    private static async void BetweenGame()
    {
        SceneManager.LoadScene(betweenGameScreen);
        await new WaitForSecondsRealtime(3);
        if (DataController.GetLives() == 0)
        {
            EndGame();
        }
        else
        {
            NextGame();
        }
    }

    public static async void EndGame()
    {
        SceneManager.LoadScene(mainMenu);

        int score = DataController.GetCurrentScore();
        if (PlayerPrefs.GetInt("highScore") < score)
        {
            PlayerPrefs.SetInt("highScore", score);
            string id = PlayerPrefs.GetString("_id");
            if (id != null && id.Length > 0)
            {
                Highscore highscore = await Highscores.Update(id, score);
                if (highscore != null) {
                    PlayerPrefs.SetInt("rank", highscore.rank);
                }
            }
        }
        DataController.Init();
    }

    // Helper functions
    private static string GetRandomGame()
    {
        string[] filteredGames = games.Where(game => game != currentGame).ToArray();
        return filteredGames[Random.Range(0, filteredGames.Length)];
    }

    public static string[] getGames()
    {
        return games;
    }

    // Debugging functions


    public static void StartDebugGame()
    {
        DataController.SetDebugMode(true);
        SceneManager.LoadScene("DebugBetweenGameScreen");
    }

    private static void DebugBetweenGame()
    {
        if (DataController.GetLives() == 0)
        {
            EndGame();
        }
        else
        {
            SceneManager.LoadScene("DebugBetweenGameScreen");
        }
    }
}