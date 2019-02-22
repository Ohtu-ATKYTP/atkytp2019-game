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
        rankText = GetComponent<Text>();
        rankText.text = "Rank: ";
        StartCoroutine(FetchAndDisplayRank());

    }

    private IEnumerator FetchAndDisplayRank(){
        while (true){
            if(PlayerPrefs.GetInt("registered") == 0){
                yield return new WaitForSecondsRealtime(60);
                continue;
            }
            rankText.text = "Rank: " + PlayerPrefs.GetInt("rank");
            webScript.GetRank();
            yield return new WaitForSecondsRealtime(60);
        }
    }
}
