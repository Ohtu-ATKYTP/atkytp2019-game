using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class RegistrationManager : MonoBehaviour {
    public Text statusMessage;

    public async void SendFormData () {
        if (PlayerPrefs.GetInt ("registered") == 1) {
            DisplayMessage ("You cannot register twice");
            return;
        }

        string userName = GameObject.Find("UsernameInput").transform.Find("Text").GetComponent<Text>().text;
        string token = GameObject.Find("TokenInput").transform.Find("Text").GetComponent<Text>().text;

        Highscore highscore = await Highscores.Create (userName, token);

        if (highscore != null) {
            PlayerPrefs.SetString ("_id", highscore._id);
            PlayerPrefs.SetString ("username", highscore.user);
            PlayerPrefs.SetString ("token", highscore.token);
            PlayerPrefs.SetInt ("highScore", highscore.score);
            PlayerPrefs.SetInt ("registered", 1);
            loadMainMenu();
        } else {
            HandleError();
        }
    }

    private void HandleError () {
        string errorString = PlayerPrefs.GetString ("error");
        ErrorCollection errorCollection = JsonUtility.FromJson<ErrorCollection> (errorString);
        string errorMessage = "";
        foreach (string error in errorCollection.error) {
            errorMessage = errorMessage + error + "\n";
        }
        DisplayMessage (errorMessage);
    }

    private void DisplayMessage (string message) {
        statusMessage.text = message;
    }

    public void loadMainMenu() {
        SceneManager.LoadScene ("MainMenu");
    }

    
}