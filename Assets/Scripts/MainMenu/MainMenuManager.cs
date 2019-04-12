using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour {

    private Text ownHighscore;

    private void Start() {
        if(!PlayerPrefs.HasKey("registered")){
            PlayerPrefs.SetInt("registered", 0);
            loadScene("Registration");
        }
        ownHighscore = GameObject.Find("OwnHighscoreText").GetComponent<Text>();
        updateOwnHighscoreAndRank();
        toggleRegistrationButton();
    }

    private async void updateOwnHighscoreAndRank() {
        int score = (PlayerPrefs.HasKey("highScore")) ?  PlayerPrefs.GetInt("highScore") : 0;
        ownHighscore.text = "High score: " + score;

        string id = PlayerPrefs.GetString ("_id");
        Highscore highscore = await Highscores.GetOne (id);
        if (highscore != null) {
            PlayerPrefs.SetInt ("rank", highscore.rank);
            PlayerPrefs.SetInt ("highScore", highscore.score);
        }
    }

    private void toggleRegistrationButton() {
        GameObject registrationButton = GameObject.Find("RegistrationButton");
        bool isVisible = PlayerPrefs.GetInt("registered") == 0 ? true: false;
        registrationButton.SetActive(isVisible);
    }

    public void startGame() {
        DataController.SetDebugMode(false);
        GameManager.startGame();
    }

    public void loadScene(string sceneName) {
        SceneManager.LoadScene (sceneName);
    }

    public void openUrl(string url){
        Application.OpenURL(url);
    }

}
