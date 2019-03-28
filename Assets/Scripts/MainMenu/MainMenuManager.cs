using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour {

    private WebServiceScript webScript;
    private DataController dataController;
    private Text ownHighscore;

    private void Start() {
        webScript = FindObjectOfType<WebServiceScript> ();
        dataController = FindObjectOfType<DataController>();
        ownHighscore = GameObject.Find("OwnHighScoreText").GetComponent<Text>();
        updateOwnHighscoreAndRank();
        toggleRegistrationButton();
    }

    public async void updateOwnHighscoreAndRank() {
        int score = (PlayerPrefs.HasKey("highScore")) ?  PlayerPrefs.GetInt("highScore") : 0;
        ownHighscore.text = "High score: " + score;

        string id = PlayerPrefs.GetString ("_id");
        HighScore highscore = await webScript.GetOne (id);
        if (highscore != null) {
            PlayerPrefs.SetInt ("rank", highscore.rank);
            PlayerPrefs.SetInt ("highScore", highscore.score);
        }
    }

    public void toggleRegistrationButton() {
        GameObject registrationButton = GameObject.Find("RegistrationButton");
        bool isVisible = PlayerPrefs.GetInt("registered") == 0 ? true: false;
        registrationButton.SetActive(isVisible);
    }

    public void startGame() {
        dataController.SetDebugMode(false);
		dataController.SetStatus(DataController.Status.MINIGAME);
    }

    public void loadScene(string sceneName) {
        SceneManager.LoadScene (sceneName, LoadSceneMode.Additive);
        SceneManager.UnloadSceneAsync ("MainMenu");
    }

}
