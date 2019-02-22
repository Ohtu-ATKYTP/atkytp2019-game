using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Debugger : MonoBehaviour{
    private HighScoreManager HSManager;

    private void Start() {
        
    }
    public void DeleteAllPlayerPrefs()
    {
        PlayerPrefs.DeleteAll();
    }

    public void DeleteID()
    {
        PlayerPrefs.DeleteKey("_id");
    }

    public void DeleteUsername()
    {
        PlayerPrefs.DeleteKey("username");
    }

    public void DeleteToken()
    {
        PlayerPrefs.DeleteKey("token");
    }

    public void DeleteHighScore()
    {
        PlayerPrefs.DeleteKey("highScore");
    }

    public void DeleteSyncedHS()
    {
        PlayerPrefs.DeleteKey("syncedHS");
    }

    public void DeleteRegistered()
    {
        PlayerPrefs.DeleteKey("registered");
    }

    public void increaseHighScore()
    {
        PlayerPrefs.SetInt("highScore", PlayerPrefs.GetInt("highScore")+10);
        PlayerPrefs.SetInt("syncedHS", 0);
        this.HSManager = FindObjectOfType<HighScoreManager>();
        HSManager.StartSync();
    }

    public void decreaseHighScore()
    {
        PlayerPrefs.SetInt("highScore",PlayerPrefs.GetInt("highScore")-10);
        PlayerPrefs.SetInt("syncedHS", 0);
        this.HSManager = FindObjectOfType<HighScoreManager>();
        HSManager.StartSync();
    }
}
