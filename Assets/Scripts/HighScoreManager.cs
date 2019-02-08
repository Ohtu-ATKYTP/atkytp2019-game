using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HighScoreManager : MonoBehaviour {
    private WebServiceScript webService;
    public int attemptTimeDelay = 60;



    void Start() {
        webService = FindObjectOfType<WebServiceScript>();

        if (!PlayerPrefs.HasKey("syncedHS")) {
            PlayerPrefs.SetInt("syncedHS", 1);
        }
        if(!PlayerPrefs.HasKey("highScore")){ 
            PlayerPrefs.SetInt("highScore", 0);  
          }
        if (PlayerPrefs.GetInt("registered") == 1 && PlayerPrefs.GetInt("syncedHS") == 0) {
                StartSync();           
        }
    }

   

    public void StartSync(){
        if(PlayerPrefs.GetInt("registered") == 1){
            StartCoroutine(Sync());
            }
        }


    private IEnumerator Sync() {
        bool success = false;
        int score = PlayerPrefs.GetInt("highScore");
        while(!success){
            webService.SendHighscore(score, (result) => {
                   success = result;
                   });  
            if(!success){
                    yield return new WaitForSeconds(attemptTimeDelay);
            }
        }
        PlayerPrefs.SetInt("syncedHS", 1);
    }
}
