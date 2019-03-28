using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour {

    private DataController dataController;
    private Dictionary<string, Canvas> screens = new Dictionary<string, Canvas>{};
    private string current = "Main Menu Screen";

    private Text ownHighscore;
    private GameObject backToRegistrationButton;

    private void Start() {
        setupScreens();
        ownHighscore = GameObject.Find("OwnHighScoreText").GetComponent<Text>();
        updateOwnHighscore();
        backToRegistrationButton = GameObject.Find("BackToRegistrationButton");
        toggleBackToRegistrationButton();
        dataController = FindObjectOfType<DataController>();
    }

    private void setupScreens() {
        screens["Main Menu Screen"] = GameObject.Find("Main Menu Screen").GetComponent<Canvas>();
        screens["Settings Screen"] = GameObject.Find("Settings Screen").GetComponent<Canvas>();

        foreach(Canvas c in screens.Values) {
            c.enabled = false;
        }
        
        screens["Main Menu Screen"].enabled = true;
        current = "Main Menu Screen";

        if(!PlayerPrefs.HasKey("registered")){
            loadRegistration();
            PlayerPrefs.SetInt("registered", 0);
        }
    }

    public void displayOnlyMenu(string canvasName) {
        screens[current].enabled = false;
        screens[canvasName].enabled = true;
        current = canvasName;
    }

    public void updateOwnHighscore() {
        int score = (PlayerPrefs.HasKey("highScore")) ?  PlayerPrefs.GetInt("highScore") : 0;
        ownHighscore.text = "High score: " + score;
    }

    public void toggleBackToRegistrationButton() {
        bool isVisible = PlayerPrefs.GetInt("registered") == 0 ? true: false;
        backToRegistrationButton.SetActive(isVisible);
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

}
