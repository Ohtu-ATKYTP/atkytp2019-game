using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour {

    private void Start() {
        if(!PlayerPrefs.HasKey("registered")){
            PlayerPrefs.SetInt("registered", 0);
            loadScene("Registration");
        }
        
        Text ownHighscore = GameObject.Find("OwnHighscoreText").GetComponent<Text>();
        int score = (PlayerPrefs.HasKey("highScore")) ?  PlayerPrefs.GetInt("highScore") : 0;
        ownHighscore.text = "High score: " + score;

        toggleRegistrationButton();
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
