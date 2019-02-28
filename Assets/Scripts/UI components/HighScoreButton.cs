using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HighScoreButton : MonoBehaviour {
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

    // This should be called from the button that activates the high score screen
    public void FetchUpdatedHighScores() {
        jsonScores = "";

        StartCoroutine(RequestUpdatedHighScoresUntilSuccess());
    }

    private IEnumerator RequestUpdatedHighScoresUntilSuccess() {
        bool success = false;
        bool attemptComplete = false;
        while (!success) {
            
            attemptComplete = false;
            webScript.GetHighscores((scores) => {
                jsonScores = scores != null ? scores : "";
                attemptComplete = true;
            });
            // Yielding until the attempt to request info has completed
            // prevents waiting for delay when attempting the first time,
            // so a successful first attempt will be displayed asap
            yield return new WaitUntil(() => attemptComplete);

            if (jsonScores.Length != 0) {
                success = true;
            } else {
                yield return new WaitForSeconds(requestDelayInSeconds);
            }
        }
        UpdateDisplay(jsonScores);
    }


    public void UpdateDisplay(string jsonScores) {

        HighScore[] highScores = JsonHelper.FromJson<HighScore>(jsonScores);

        string usernameInfo = "";
        string scoreInfo = "";

        string playerStyleOn = "<color=red><b>";
        string playerStyleOff = "</b></color>";

        bool isUserinTop10 = false;
        int i = 0;
        foreach (HighScore hs in highScores) {
            
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



