using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour {

    private void Start() {
        if(!PlayerPrefs.HasKey("registered")){
            PlayerPrefs.SetInt("registered", 0);
            LoadScene("Registration");
        }
        
        Text ownHighscore = GameObject.Find("OwnHighscoreText").GetComponent<Text>();
        int score = (PlayerPrefs.HasKey("highScore")) ?  PlayerPrefs.GetInt("highScore") : 0;
        ownHighscore.text = "High score: " + score;

        ToggleRegistrationButton();
    }

    private void ToggleRegistrationButton() {
        GameObject registrationButton = GameObject.Find("RegistrationButton");
        bool isVisible = PlayerPrefs.GetInt("registered") == 0 ? true: false;
        registrationButton.SetActive(isVisible);
    }

    public void StartGame() {
        DataController.SetDebugMode(false);
        GameManager.StartGame();
    }

    public void LoadScene(string sceneName) {
        SceneManager.LoadScene (sceneName);
    }

    public void OpenUrl(string url){
        Application.OpenURL(url);
    }

}
