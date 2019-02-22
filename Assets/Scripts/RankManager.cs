using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class RankManager : MonoBehaviour {
    private WebServiceScript webScript;
    void Start() {
        webScript = FindObjectOfType<WebServiceScript>();
        StartCoroutine(FetchAndStoreRank());
    }
    private IEnumerator FetchAndStoreRank(){
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
        yield return new WaitForSecondsRealtime(3);
        webScript.GetRank();
        
    }

}
