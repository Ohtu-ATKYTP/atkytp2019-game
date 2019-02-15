using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using System.Text;

public class WebServiceScript : MonoBehaviour {
    private string baseUrl = "http://atkytpgame.herokuapp.com/api/highscores";

    [HideInInspector]
    public string highscores;

    void Start() {
        GetHighscores();
        /*
         * If you want to see that updating high score on server works, uncomment
        if (PlayerPrefs.HasKey("highScore")) {
            StartCoroutine(SendScore(69));
        }
        */
    }


    public void GetHighscores(System.Action<string> callback) {
        StartCoroutine(GetHighscoresText(callback));
    }

    //Use this method to fetch highscores
    public void GetHighscores() {
        StartCoroutine(GetHighscoresText(res => {
            }));
    }

    //Use this method to add new users
    public void RegisterUser(string user, string token, System.Action<bool, bool, string> callback) {
        StartCoroutine(SendUser(user, token, callback));
    }

    public void SendHighscore(int score, System.Action<bool> callback){
            StartCoroutine(SendScore(score, callback));
    }

    private IEnumerator GetHighscoresText(System.Action<string> callback) {
        UnityWebRequest req = UnityWebRequest.Get(baseUrl + "/top");
        yield return req.SendWebRequest();

        if (req.isNetworkError || req.isHttpError) {
            Debug.Log(req.error);
        } else {
            // Show results as text
            highscores = req.downloadHandler.text;

            // Or retrieve results as binary data
            byte[] results = req.downloadHandler.data;

        }
        callback(highscores);
    }

    private IEnumerator SendUser(string user, string token, System.Action<bool, bool, string> callback) {
        UnityWebRequest req = new UnityWebRequest(baseUrl, "POST");
        string jsonUser = JsonifyUser(user, token, 0);
        byte[] bodyRaw = Encoding.UTF8.GetBytes(jsonUser);
        req.uploadHandler = (UploadHandler)new UploadHandlerRaw(bodyRaw);
        req.downloadHandler = (DownloadHandler)new DownloadHandlerBuffer();
        req.SetRequestHeader("Content-Type", "application/json");

        yield return req.SendWebRequest();



        if(req.isHttpError){ 
            callback(true, false, req.downloadHandler.text);
        } else if(req.isNetworkError){ 
            callback(false, false, "");
        } else {  
        jsonUser = req.downloadHandler.text;
        HighScore h = JsonUtility.FromJson<HighScore>(jsonUser);
        
        PlayerPrefs.SetString("_id", h._id);
        PlayerPrefs.SetString("username", h.user);
        PlayerPrefs.SetString("token", h.token);
        PlayerPrefs.SetInt("highScore", h.score);
        PlayerPrefs.SetInt("syncedHS", 1);
        callback(true, true, "");
        }
        Debug.Log("------\nSaatu data:" + jsonUser);
        Debug.Log("Status code: " + req.responseCode);
    }

    private IEnumerator SendScore(int score,  System.Action<bool> callback) {
        // should we account for the possibility of not existing?
        string id = PlayerPrefs.GetString("_id");
        string url = baseUrl + "/" + id;
        UnityWebRequest req = new UnityWebRequest(url, "PUT");
        string jsonHighScore = "{ \"score\": " + score + "}";
        byte[] bodyRaw = Encoding.UTF8.GetBytes(jsonHighScore);

        req.uploadHandler = (UploadHandler)new UploadHandlerRaw(bodyRaw);
        req.downloadHandler = (DownloadHandler)new DownloadHandlerBuffer();
        req.SetRequestHeader("Content-Type", "application/json");
        yield return req.SendWebRequest();

        
        if(req.isNetworkError){ 
                callback(false);            
         } else {
            HighScore h = JsonUtility.FromJson<HighScore>(req.downloadHandler.text);
            //alempi lienee toivottavaa poistaa
            PlayerPrefs.SetInt("highScore", h.score);
            callback(true);
        }
            
    }

    public void GetRank()
    {
        StartCoroutine(GetRankCOR());
    }

    public IEnumerator GetRankCOR() {

        string id = PlayerPrefs.GetString("_id");
        string url = baseUrl + "/" + id;

        UnityWebRequest req = UnityWebRequest.Get(url);

        yield return req.SendWebRequest();

        if (req.isNetworkError || req.isHttpError)
        {
            Debug.Log(req.error);
        }
        
        else
        {
            HighScore h = JsonUtility.FromJson<HighScore>(req.downloadHandler.text);
            PlayerPrefs.SetInt("rank", h.rank);
        }
    }


    private string JsonifyUser(string user, string token) {
        return JsonifyUser(user, token, 0);
    }

    private string JsonifyUser(string user, string token, int score)
    {
        return JsonifyUser(user, token, 0, 0);
    }

    private string JsonifyUser(string user, string token, int score, int rank) {
        HighScore h = new HighScore();
        h.user = user;
        h.token = token;
        h.score = score;
        h.rank = rank;
        return JsonUtility.ToJson(h);
    }


}
