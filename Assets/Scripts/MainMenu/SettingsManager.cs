using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SettingsManager : MonoBehaviour {

    private Text playerInfo;

    private int score;

    void Start() {
        playerInfo = GameObject.Find("PlayerInfo").GetComponent<Text>();
        score = PlayerPrefs.GetInt("highScore");
        updatePlayerInfo();
    }

    private void updatePlayerInfo() {
        string info = "Username: "+ PlayerPrefs.GetString("username")+"\n";
        info += "ID: " + PlayerPrefs.GetString("_id") + "\n";
        info += "Token: " + PlayerPrefs.GetString("token") + "\n";
        info += "Highscore: " + score + "\n";
        info += "Registered: " + ((PlayerPrefs.GetInt("registered") == 1) ? "yes" : "no")+ "\n";
        info += "Rank: " + PlayerPrefs.GetInt("rank") + "\n";

        playerInfo.text = info;
    }

    public void DeleteAllPlayerPrefs() {
        PlayerPrefs.DeleteAll();
        score = 0;
        updatePlayerInfo();
    }

    public void DeleteID() {
        PlayerPrefs.DeleteKey("_id");
        updatePlayerInfo();
    }

    public void DeleteUsername() {
        PlayerPrefs.DeleteKey("username");
        updatePlayerInfo();
    }

    public void DeleteToken() {
        PlayerPrefs.DeleteKey("token");
        updatePlayerInfo();
    }

    public void DeleteHighscore() {
        PlayerPrefs.DeleteKey("highScore");
        updatePlayerInfo();
    }

    public void DeleteRegistered() {
        PlayerPrefs.DeleteKey("registered");
        updatePlayerInfo();
    }

    public void increaseHighscore() {
        score += 10;
        updatePlayerInfo();
    }

    public void decreaseHighscore() {
        score -= 10;
        updatePlayerInfo();
    }
    
    public async void syncHS() {
        Highscore highscore = await Highscores.Update (PlayerPrefs.GetString("_id"), score);
        PlayerPrefs.SetInt("highScore", highscore.score);
        PlayerPrefs.SetInt("rank", highscore.rank);
        updatePlayerInfo();
    }

    public void loadMainMenu() {
        SceneManager.LoadScene ("MainMenu");
    }

    
}
