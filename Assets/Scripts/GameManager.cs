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

    public static void startGame()
    {
        DataController.Init();
        nextGame();
    }

    public static void nextGame(string game = "Random")
    {
        if (game == "Random")
        {
            game = getRandomGame();
        }
        SceneManager.LoadScene(game);
        currentGame = game;
    }

    public static void endMinigame(bool win)
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
            betweenGame();
        }
        else
        {
            debugBetweenGame();
        }
    }

    private static async void betweenGame()
    {
        SceneManager.LoadScene(betweenGameScreen);
        await new WaitForSecondsRealtime(3);
        if (DataController.GetLives() == 0)
        {
            endGame();
        }
        else
        {
            nextGame();
        }
    }

    public static async void endGame()
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
    private static string getRandomGame()
    {
        string[] filteredGames = games.Where(game => game != currentGame).ToArray();
        return filteredGames[Random.Range(0, filteredGames.Length)];
    }

    public static string[] getGames()
    {
        return games;
    }

    // Debugging functions

    public static void startDebugGame()
    {
        DataController.SetDebugMode(true);
        SceneManager.LoadScene("DebugBetweenGameScreen");
    }

    private static void debugBetweenGame()
    {
        if (DataController.GetLives() == 0)
        {
            endGame();
        }
        else
        {
            SceneManager.LoadScene("DebugBetweenGameScreen");
        }
    }
}