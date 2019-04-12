using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class HighscoreManager : MonoBehaviour {
    private Text usernames;
    private Text scores;

    void Start() {
        Text[] textComponents = GetComponentsInChildren<Text>();
        for (int i = 0; i < textComponents.Length; i++) {
            Text texComp = textComponents[i];
            if (texComp.name == "PlayersText") {
                usernames = texComp;
            } else if (texComp.name == "ScoresText") {
                scores = texComp;
            }
        }
        usernames.text = PlayerPrefs.GetString("usernameInfo");
        scores.text = PlayerPrefs.GetString("scoreInfo");
        FetchUpdatedHighscores();
    }

    private async void FetchUpdatedHighscores() {
        Highscore[] highscores = await Highscores.GetTop10();

        string usernameInfo = "";
        string scoreInfo = "";

        string playerStyleOn = "<color=red><b>";
        string playerStyleOff = "</b></color>";

        bool isUserinTop10 = false;
        int i = 1;
        foreach (Highscore hs in highscores) {
            
            if(hs.user == PlayerPrefs.GetString("username")){
                isUserinTop10 = true;
                usernameInfo +=  playerStyleOn + i + ". "+hs.user + 
                playerStyleOff + "\n";
                
                scoreInfo += playerStyleOn + hs.score + 
                playerStyleOff + "\n";
            
            } else {
            usernameInfo += i + ". " + hs.user + "\n";
            scoreInfo += hs.score + "\n";
            }
            i++;
        }

        if(!isUserinTop10 && PlayerPrefs.HasKey("username")){
            
            usernameInfo += "\n";
            scoreInfo += "\n";
            
            usernameInfo += playerStyleOn + PlayerPrefs.GetInt("rank") + ". " 
            + PlayerPrefs.GetString("username") + playerStyleOff;
            
            scoreInfo += playerStyleOn + PlayerPrefs.GetInt("highScore")
            + playerStyleOff;
        }

        usernames.text = usernameInfo;
        scores.text = scoreInfo;

        PlayerPrefs.SetString("usernameInfo", usernameInfo);
        PlayerPrefs.SetString("scoreInfo", scoreInfo);
    }

    public void loadMainMenu() {
        SceneManager.LoadScene ("MainMenu");
    }
}



