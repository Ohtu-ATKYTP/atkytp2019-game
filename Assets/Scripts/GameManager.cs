using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Linq;

public static class GameManager {
    private static string currentGame;
    private static string[] games = {"PlaceCity", "TurkuGame", "LogoHaalariin", "ElevatorGame", "Tamperelainen"};
    private static string betweenGameScreen = "BetweenGameScreen";
    private static string mainMenu = "MainMenu";

    public static void startGame() {
        DataController.Init();
        nextGame();
    }

    public static void nextGame(string game = "Random") {
        if (game == "Random") {
            game = getRandomGame();
        }
        SceneManager.LoadScene(game);
        currentGame = game;
    }

    public static void endMinigame(bool win, int score) {
        DataController.SetWinStatus(win);
        if (!win) {
            DataController.TakeLife();
        }
        DataController.AddCurrentScore(score);
		DataController.incrementRoundsCompleted();
		DataController.UpdateDifficulty();

        // Debug mode checker
        if (!DataController.GetDebugMode()) {
            betweenGame();
        } else {
            dubugBetweenGame();
        }
    }

    private static async void betweenGame() {
        SceneManager.LoadScene(betweenGameScreen);
        await new WaitForSecondsRealtime(3);
        if (DataController.GetLives() == 0) {
            endGame();
        } else {
            nextGame();
        }
    }

    public static async void endGame() {
        SceneManager.LoadScene(mainMenu);

        int score = DataController.GetCurrentScore();
        if (PlayerPrefs.GetInt ("highScore") < score) {
            PlayerPrefs.SetInt ("highScore", score);
            string id = PlayerPrefs.GetString ("_id");
            await Highscores.Update (id, score);
        }
        DataController.Init();
    }

    // Helper functions
    private static string getRandomGame() {
        string[] filteredGames = games.Where(game => game != currentGame).ToArray();
        return filteredGames[Random.Range(0, filteredGames.Length)];
    }

    public static string[] getGames() {
        return games;
    }

    // Debugging functions

    public static void startDebugGame() {
        DataController.SetDebugMode(true);
        SceneManager.LoadScene("DebugBetweenGameScreen");
    }

    private static void dubugBetweenGame() {
        if (DataController.GetLives() == 0) {
            endGame();
        }
        SceneManager.LoadScene("DebugBetweenGameScreen");
    }
}