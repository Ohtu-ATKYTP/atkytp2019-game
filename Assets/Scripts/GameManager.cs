using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Linq;

public static class GameManager {
    public static string currentGame;
    private static string[] games = {"PlaceCity", "TurkuGame", "LogoHaalariin", "ElevatorGame"};
    private static string betweenGameScreen = "BetweenGameScreen";
    private static string mainMenu = "MainMenu";
    public static void startGame() {
        DataController.Init();
        string firstGame = getRandomGame();
        SceneManager.LoadScene(firstGame, LoadSceneMode.Additive);
        currentGame = firstGame;
        SceneManager.UnloadSceneAsync(mainMenu);
    }

    private static string getRandomGame() {
        string[] filteredGames = games.Where(game => game != currentGame).ToArray();
        return filteredGames[Random.Range(0, filteredGames.Length)];
    }

    public static void endMinigame(bool win, int score) {
        DataController.SetWinStatus(win);
        if (!win) {
            DataController.TakeLife();
        }
        DataController.AddCurrentScore(score);
		DataController.incrementRoundsCompleted();
		DataController.UpdateDifficulty();

        betweenGame();
    }

    private static void betweenGame() {

        SceneManager.LoadScene(betweenGameScreen, LoadSceneMode.Additive);
        SceneManager.UnloadSceneAsync(currentGame);

        if (DataController.GetLives() == 0) {
            endGame();
        } else {
            string nextGame = getRandomGame();
            SceneManager.LoadScene(nextGame, LoadSceneMode.Additive);
            SceneManager.UnloadSceneAsync(betweenGameScreen);
        }
    }

    private static async void endGame() {
        SceneManager.LoadScene(mainMenu, LoadSceneMode.Additive);
        SceneManager.UnloadSceneAsync(currentGame);

        int score = DataController.GetCurrentScore();
        if (PlayerPrefs.GetInt ("highScore") < score) {
            PlayerPrefs.SetInt ("highScore", score);
            string id = PlayerPrefs.GetString ("_id");
            Highscore updated = await Highscores.Update (id, score);
        }
    }
}