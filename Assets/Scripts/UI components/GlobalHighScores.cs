using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GlobalHighScores : MonoBehaviour {
    private WebServiceScript webScript;
    private Text usernames; 
    private Text scores;
    private string jsonScores;



     void Start() {
       webScript = FindObjectOfType<WebServiceScript>();
       Text[] textComponents = GetComponentsInChildren<Text>();
       for(int i = 0; i < textComponents.Length; i++){
                Text texComp = textComponents[i];
                if(texComp.name == "Players"){
                    usernames = texComp;
                } else if(texComp.name == "Scores"){ 
                    scores = texComp;
                }
            }
            StartCoroutine(ShowLoadingMessage());
    }

    // This should be called from the button that activates the high score screen
    public void FetchUpdatedHighScores(){
            jsonScores = "";
        // callback as parameter; ultimately the coroutine that retrieves data from server
        // will call this with the retrieved data as parameter
            webScript.GetHighscores((scores) => {
                    jsonScores = scores;
                    UpdateDisplay(scores);
                });            
        }


    public void UpdateDisplay(string jsonScores){ 
         HighScore[] highScores = JsonHelper.FromJson<HighScore>(jsonScores);

         string usernameInfo = ""; 
         string scoreInfo = ""; 
         
         for(int i = 0; i < highScores.Length; i++){
            usernameInfo += highScores[i].user + "\n";
            scoreInfo += highScores[i].score + "\n";
         }
            

         usernames.text = usernameInfo;
         scores.text = scoreInfo;
         

        }


    private IEnumerator ShowLoadingMessage(){
            yield return new WaitForSeconds(1);
            if(jsonScores == null || jsonScores.Length == 0){ 
                usernames.text = "Loading...";
            }

    }


}
