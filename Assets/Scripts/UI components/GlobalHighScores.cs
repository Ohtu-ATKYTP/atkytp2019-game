using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GlobalHighScores : MonoBehaviour {
    private WebServiceScript webScript;
    private Text usernames;
    private Text scores;
    private string jsonScores;
    public int requestDelayInSeconds = 60;



    void Start() {
        webScript = FindObjectOfType<WebServiceScript>();
        Text[] textComponents = GetComponentsInChildren<Text>();
        for (int i = 0; i < textComponents.Length; i++) {
            Text texComp = textComponents[i];
            if (texComp.name == "Players") {
                usernames = texComp;
            } else if (texComp.name == "Scores") {
                scores = texComp;
            }
        }
    }

    public async void FetchUpdatedHighScores() {
        HighScore[] highscores = await webScript.GetTop10();

        string usernameInfo = "";
        string scoreInfo = "";

        string playerStyleOn = "<color=red><b>";
        string playerStyleOff = "</b></color>";

        bool isUserinTop10 = false;
        int i = 0;
        foreach (HighScore hs in highscores) {
            
            if(hs.user == PlayerPrefs.GetString("username")){
                isUserinTop10 = true;
                int spot = i+1;
                usernameInfo +=  playerStyleOn + spot + ". "+hs.user + 
                playerStyleOff + "\n";
                
                scoreInfo += playerStyleOn + hs.score + 
                playerStyleOff + "\n";
            
            }else{
            usernameInfo += i+1 + ". " + hs.user + "\n";
            scoreInfo += hs.score + "\n";
            }
            i++;
        }

        if(!isUserinTop10){
            
            usernameInfo += "------------\n";
            scoreInfo += "---\n";
            
            usernameInfo += playerStyleOn + PlayerPrefs.GetInt("rank") + ". " 
            + PlayerPrefs.GetString("username") + playerStyleOff;
            
            scoreInfo += playerStyleOn + PlayerPrefs.GetInt("highScore")
            + playerStyleOff;
        }
        usernames.text = usernameInfo;
        scores.text = scoreInfo;
    }
}



