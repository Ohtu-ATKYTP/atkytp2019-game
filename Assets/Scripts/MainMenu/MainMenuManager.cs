using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour {

    private DataController dataController;
    private Text ownHighscore;
    private GameObject registrationButton;

    private void Start() {
        ownHighscore = GameObject.Find("OwnHighScoreText").GetComponent<Text>();
        updateOwnHighscore();
        registrationButton = GameObject.Find("RegistrationButton");
        toggleRegistrationButton();
        dataController = FindObjectOfType<DataController>();
    }

    public void updateOwnHighscore() {
        int score = (PlayerPrefs.HasKey("highScore")) ?  PlayerPrefs.GetInt("highScore") : 0;
        ownHighscore.text = "High score: " + score;
    }

    public void toggleRegistrationButton() {
        bool isVisible = PlayerPrefs.GetInt("registered") == 0 ? true: false;
        registrationButton.SetActive(isVisible);
    }

    public void startGame() {
        dataController.SetDebugMode(false);
		dataController.SetStatus(DataController.Status.MINIGAME);
    }

    public async void loadHighscores() {
        SceneManager.LoadScene ("Highscores", LoadSceneMode.Additive);
        await SceneManager.UnloadSceneAsync ("MainMenu");
    }

    public async void loadRegistration() {
        SceneManager.LoadScene ("Registration", LoadSceneMode.Additive);
        await SceneManager.UnloadSceneAsync ("MainMenu");
    }

    public async void loadSettings() {
        SceneManager.LoadScene ("Settings", LoadSceneMode.Additive);
        await SceneManager.UnloadSceneAsync ("MainMenu");
    }

}
