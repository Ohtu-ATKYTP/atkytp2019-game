using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SettingsManager : MonoBehaviour {

    private Text playerInfo;

    private int score;

    void Start() {
        playerInfo = GameObject.Find("PlayerInfo").GetComponent<Text>();
        score = PlayerPrefs.GetInt("highScore");
        UpdatePlayerInfo();
    }

    private void UpdatePlayerInfo() {
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
        UpdatePlayerInfo();
    }

    public void DeleteID() {
        PlayerPrefs.DeleteKey("_id");
        UpdatePlayerInfo();
    }

    public void DeleteUsername() {
        PlayerPrefs.DeleteKey("username");
        UpdatePlayerInfo();
    }

    public void DeleteToken() {
        PlayerPrefs.DeleteKey("token");
        UpdatePlayerInfo();
    }

    public void DeleteHighscore() {
        PlayerPrefs.DeleteKey("highScore");
        UpdatePlayerInfo();
    }

    public void DeleteRegistered() {
        PlayerPrefs.DeleteKey("registered");
        UpdatePlayerInfo();
    }

    public void IncreaseHighscore() {
        score += 10;
        UpdatePlayerInfo();
    }

    public void DecreaseHighscore() {
        score -= 10;
        UpdatePlayerInfo();
    }
    
    public async void SyncHS() {
        Highscore highscore = await Highscores.Update (PlayerPrefs.GetString("_id"), score);
        PlayerPrefs.SetInt("highScore", highscore.score);
        PlayerPrefs.SetInt("rank", highscore.rank);
        UpdatePlayerInfo();
    }

    public void LoadMainMenu() {
        SceneManager.LoadScene ("MainMenu");
    }

    
}
