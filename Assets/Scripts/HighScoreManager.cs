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
        if (PlayerPrefs.GetInt("syncedHS") == 0) {
            StartSync();
        }
    }

   

    public void StartSync(){
            StartCoroutine(Sync());
        }


    private IEnumerator Sync() {
        bool success = false;
        int score = PlayerPrefs.GetInt("highScore");
        while(!success){
            Debug.Log("Yritetään lähettää score " + score);
            Debug.Log("Onko webservice?" + (webService != null));
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
