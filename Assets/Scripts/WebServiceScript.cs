using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Networking;

public class WebServiceScript : MonoBehaviour {
    private string baseUrl = "http://atkytpgame.herokuapp.com/api/highscores";

    public async Task<HighScore[]> GetTop10 () {
        string url = baseUrl + "/top";
        string json = await GetRequest (url);

        if (json.Length == 0) {
            return new HighScore[0];
        } else {
            return JsonHelper.FromJson<HighScore> (json);
        }
    }

    public async Task<HighScore> GetOne (string id) {
        string url = baseUrl + "/" + id;
        string json = await GetRequest (url);
        if (json.Length == 0) {
            return null;
        } else {
            return JsonUtility.FromJson<HighScore> (json);
        }
    }

    public async Task<HighScore> Create (string user, string token) {
        string jsonHighscore = JsonifyUser (user, token);
        string json = await PostRequest(baseUrl, jsonHighscore);
        if (json.Length == 0) {
            return null;
        } else {
            return JsonUtility.FromJson<HighScore> (json);
        }
    }

    public async Task<HighScore> Update (string id, int score) {
        string url = baseUrl + "/" + id;
        string jsonScore = "{ \"score\": " + score + "}";
        string json = await PutRequest(url, jsonScore);
        if (json.Length == 0) {
            return null;
        } else {
            return JsonUtility.FromJson<HighScore> (json);
        }
    }

    private async Task<string> GetRequest (string url) {
        UnityWebRequest req = UnityWebRequest.Get (url);
        await req.SendWebRequest ();
        if (req.isNetworkError || req.isHttpError) {
            Debug.Log (req.error);
            return "";
        } else {
            return req.downloadHandler.text;
        }
    }

    private async Task<string> PostRequest (string url, string body) {
        UnityWebRequest req = new UnityWebRequest (url, "POST");
        byte[] bodyRaw = Encoding.UTF8.GetBytes (body);
        req.uploadHandler = (UploadHandler) new UploadHandlerRaw (bodyRaw);
        req.downloadHandler = (DownloadHandler) new DownloadHandlerBuffer ();
        req.SetRequestHeader ("Content-Type", "application/json");

        await req.SendWebRequest ();

        if (req.isNetworkError || req.isHttpError) {
            Debug.Log(req.downloadHandler.text);
            return "";
        } else {
            return req.downloadHandler.text;
        }
    }

    private async Task<string> PutRequest (string url, string body) {
        UnityWebRequest req = new UnityWebRequest (url, "PUT");
        byte[] bodyRaw = Encoding.UTF8.GetBytes (body);
        req.uploadHandler = (UploadHandler) new UploadHandlerRaw (bodyRaw);
        req.downloadHandler = (DownloadHandler) new DownloadHandlerBuffer ();
        req.SetRequestHeader ("Content-Type", "application/json");

        await req.SendWebRequest ();

        if (req.isNetworkError || req.isHttpError) {
            Debug.Log(req.downloadHandler.text);
            return "";
        } else {
            return req.downloadHandler.text;
        }
    }


    //Use this method to add new users
    public void RegisterUser (string user, string token, System.Action<bool, bool, string> callback) {
        StartCoroutine (SendUser (user, token, callback));
    }

    public void SendHighscore (int score, System.Action<bool> callback) {
        StartCoroutine (SendScore (score, callback));
    }

    private IEnumerator SendUser (string user, string token, System.Action<bool, bool, string> callback) {
        UnityWebRequest req = new UnityWebRequest (baseUrl, "POST");
        string jsonUser = JsonifyUser (user, token, 0);
        byte[] bodyRaw = Encoding.UTF8.GetBytes (jsonUser);
        req.uploadHandler = (UploadHandler) new UploadHandlerRaw (bodyRaw);
        req.downloadHandler = (DownloadHandler) new DownloadHandlerBuffer ();
        req.SetRequestHeader ("Content-Type", "application/json");

        yield return req.SendWebRequest ();

        if (req.isHttpError) {
            callback (true, false, req.downloadHandler.text);
        } else if (req.isNetworkError) {
            callback (false, false, "");
        } else {
            jsonUser = req.downloadHandler.text;
            HighScore h = JsonUtility.FromJson<HighScore> (jsonUser);

            PlayerPrefs.SetString ("_id", h._id);
            PlayerPrefs.SetString ("username", h.user);
            PlayerPrefs.SetString ("token", h.token);
            PlayerPrefs.SetInt ("highScore", h.score);
            PlayerPrefs.SetInt ("syncedHS", 1);
            callback (true, true, "");
        }
        Debug.Log ("------\nSaatu data:" + jsonUser);
        Debug.Log ("Status code: " + req.responseCode);
    }

    private IEnumerator SendScore (int score, System.Action<bool> callback) {
        // should we account for the possibility of not existing?
        string id = PlayerPrefs.GetString ("_id");
        string url = baseUrl + "/" + id;
        UnityWebRequest req = new UnityWebRequest (url, "PUT");
        string jsonHighScore = "{ \"score\": " + score + "}";
        byte[] bodyRaw = Encoding.UTF8.GetBytes (jsonHighScore);

        req.uploadHandler = (UploadHandler) new UploadHandlerRaw (bodyRaw);
        req.downloadHandler = (DownloadHandler) new DownloadHandlerBuffer ();
        req.SetRequestHeader ("Content-Type", "application/json");
        yield return req.SendWebRequest ();

        if (req.isNetworkError) {
            callback (false);
        } else {
            HighScore h = JsonUtility.FromJson<HighScore> (req.downloadHandler.text);
            //alempi lienee toivottavaa poistaa
            PlayerPrefs.SetInt ("highScore", h.score);
            callback (true);
        }
    }

    public void GetRank () {
        StartCoroutine (GetRankCOR ());
    }

    public IEnumerator GetRankCOR () {

        string id = PlayerPrefs.GetString ("_id");
        string url = baseUrl + "/" + id;

        UnityWebRequest req = UnityWebRequest.Get (url);

        yield return req.SendWebRequest ();

        if (req.isNetworkError || req.isHttpError) {
            Debug.Log (req.error);
        } else {
            HighScore h = JsonUtility.FromJson<HighScore> (req.downloadHandler.text);
            PlayerPrefs.SetInt ("rank", h.rank);
        }

    }
    private string JsonifyUser (string user, string token) {
        return JsonifyUser (user, token, 0);
    }

    private string JsonifyUser (string user, string token, int score) {
        return JsonifyUser (user, token, 0, 0);
    }
    private string JsonifyUser (string user, string token, int score, int rank) {
        HighScore h = new HighScore ();
        h.user = user;
        h.token = token;
        h.score = score;
        h.rank = rank;
        return JsonUtility.ToJson (h);
    }
}