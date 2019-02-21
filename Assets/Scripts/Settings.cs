using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Settings : MonoBehaviour {
    private Text playerInfo;

    void Start() {
        playerInfo = GameObject.Find("PlayerInfo").GetComponent<Text>();
    }

    private void Update()
    {
        updatePlayerInfo();
    }

    public void updatePlayerInfo() {
        string info = "Username: "+ PlayerPrefs.GetString("username")+"\n";
        info += "ID: " + PlayerPrefs.GetString("_id") + "\n";
        info += "Token: " + PlayerPrefs.GetString("token") + "\n";
        info += "HighScore: " + PlayerPrefs.GetInt("highScore") + "\n";
        info += "HS Synced: " + ((PlayerPrefs.GetInt("syncedHS") == 1) ? "yes" : "no") + "\n";
        info += "Registered: " + ((PlayerPrefs.GetInt("registered") == 1) ? "yes" : "no");

        playerInfo.text = info;
    }
}
