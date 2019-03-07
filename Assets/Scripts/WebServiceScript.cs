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

    public async Task<HighScore> CreateHighscore (string user, string token) {
        string jsonHighscore = JsonifyUser (user, token);
        string json = await PostRequest (baseUrl, jsonHighscore);
        if (json.Length == 0) {
            return null;
        } else {
            return JsonUtility.FromJson<HighScore> (json);
        }
    }

    public async Task<HighScore> UpdateHighscore (string id, int score) {
        string url = baseUrl + "/" + id;
        string jsonScore = "{ \"score\": " + score + "}";
        string json = await PutRequest (url, jsonScore);
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

        if (req.isNetworkError) {
            String error = "No connection try again later";
            Debug.Log (error);
            return "";
        } else if (req.isHttpError) {
            String error = req.downloadHandler.text;
            Debug.Log (error);
            PlayerPrefs.SetString ("error", error);
            return "";
        }
        else {
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
            Debug.Log (req.downloadHandler.text);
            return "";
        } else {
            return req.downloadHandler.text;
        }
    }

    public void SendHighscore (int score, System.Action<bool> callback) {
      StartCoroutine (SendScore (score, callback));
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