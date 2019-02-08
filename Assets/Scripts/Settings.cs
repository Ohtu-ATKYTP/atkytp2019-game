using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Settings : MonoBehaviour {
    private Text playerInfo;

    void Start() {
        playerInfo = GameObject.Find("PlayerInfo").GetComponent<Text>();
    }
    
    public void updatePlayerInfo() {
        string info = "Username: "+ PlayerPrefs.GetString("username")+"\n";
        info += "Token: " + PlayerPrefs.GetString("token") + "\n";
        info += "HighScore: " + PlayerPrefs.GetInt("highScore") + "\n";
        info += "Registered: " + ((PlayerPrefs.GetInt("registered") == 1) ? "yes" : "no");

        playerInfo.text = info;
    }
}
