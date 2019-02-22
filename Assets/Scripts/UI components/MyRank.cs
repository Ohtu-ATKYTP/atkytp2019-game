using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MyRank : MonoBehaviour {
    private WebServiceScript webScript;
    public int requestDelayInSeconds = 60;
    private Text rankText;

    void Start() {
        webScript = FindObjectOfType<WebServiceScript>();
        this.rankText = GetComponent<Text>();
        rankText.text = "Rank: ";
        StartCoroutine(FetchAndDisplayRank());
    }

    void Update() {
        rankText.text = "Rank: " + PlayerPrefs.GetInt("rank");
    }

    private IEnumerator FetchAndDisplayRank(){
        while (true){
            if(PlayerPrefs.GetInt("registered") == 0){
                yield return new WaitForSecondsRealtime(60);
                continue;
            }
            webScript.GetRank();
            yield return new WaitForSecondsRealtime(60);
        }
    }
    public void AfterGameRank(){
        StartCoroutine(AfterGameRankCOR());
    }

    private IEnumerator AfterGameRankCOR(){
        webScript.GetRank();
        yield return new WaitForSeconds(1);
    }


}
