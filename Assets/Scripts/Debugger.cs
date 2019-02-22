using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Debugger : MonoBehaviour{
    private HighScoreManager HSManager;
    private MyRank rankManager;

    private void Start() {
        this.rankManager = FindObjectOfType<MyRank>();
        this.HSManager = FindObjectOfType<HighScoreManager>();
    }
    public void DeleteAllPlayerPrefs() {
        PlayerPrefs.DeleteAll();
    }

    public void DeleteID() {
        PlayerPrefs.DeleteKey("_id");
    }

    public void DeleteUsername() {
        PlayerPrefs.DeleteKey("username");
    }

    public void DeleteToken() {
        PlayerPrefs.DeleteKey("token");
    }

    public void DeleteHighScore() {
        PlayerPrefs.DeleteKey("highScore");
    }

    public void DeleteSyncedHS() {
        PlayerPrefs.DeleteKey("syncedHS");
    }

    public void DeleteRegistered() {
        PlayerPrefs.DeleteKey("registered");
    }

    public void increaseHighScore() {
        PlayerPrefs.SetInt("highScore", PlayerPrefs.GetInt("highScore")+10);
        PlayerPrefs.SetInt("syncedHS", 0);
    }

    public void decreaseHighScore() {
        PlayerPrefs.SetInt("highScore",PlayerPrefs.GetInt("highScore")-10);
        PlayerPrefs.SetInt("syncedHS", 0);
    }

    public void syncHighScore() {
        HSManager.StartSync();
    }

    public void updateRank() {
        rankManager.AfterGameRank();
    }

}
