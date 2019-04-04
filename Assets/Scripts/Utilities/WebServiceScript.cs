using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Networking;

public class WebServiceScript : MonoBehaviour {
    private string baseUrl = "https://atkytpgame.herokuapp.com/api/highscores";

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
        if (id == null || id.Length == 0) {
            return null;
        }
        string url = baseUrl + "/" + id;
        string json = await GetRequest (url);
        if (json.Length == 0) {
            return null;
        } else {
            return JsonUtility.FromJson<HighScore> (json);
        }
    }

    public async Task<HighScore> CreateHighscore (string user, string token) {
        HighScore h = new HighScore ();
        h.user = user;
        h.token = token;

        string jsonHighscore = JsonUtility.ToJson (h);
        string json = await PostRequest (baseUrl, jsonHighscore);
        if (json.Length == 0) {
            return null;
        } else {
            return JsonUtility.FromJson<HighScore> (json);
        }
    }

    public async Task<HighScore> UpdateHighscore (string id, int score) {
        if (id == null || id.Length == 0 || score == null) {
            return null;
        }
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

        await req.SendWebRequest();

        if (req.isNetworkError || req.isHttpError) {
            Debug.Log (req.downloadHandler.text);
            return "";
        } else {
            return req.downloadHandler.text;
        }
    }
}