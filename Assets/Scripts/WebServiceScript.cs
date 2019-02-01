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
  }


  //Use this method to fetch highscores
  public void GetHighscores() {
    StartCoroutine(GetHighscoresText());
  }

  //Use this method to add new users
  public void RegisterUser(string user, string token) {
    StartCoroutine(SendUser(user, token));
  }

  //Use this method to send a highscore to the server
  public void SendHighscore(string user, string token, int score) {
    StartCoroutine(SendScore(user, token, score));
  }

  private IEnumerator GetHighscoresText() {
    UnityWebRequest req = UnityWebRequest.Get(baseUrl);
    yield return req.SendWebRequest();

    if(req.isNetworkError || req.isHttpError) {
      Debug.Log(req.error);
    }
    else {
      // Show results as text
      Debug.Log(req.downloadHandler.text);
      highscores = req.downloadHandler.text;

      // Or retrieve results as binary data
      byte[] results = req.downloadHandler.data;
    }
  }

  private IEnumerator SendUser(string user, string token) {
    UnityWebRequest req = new UnityWebRequest(baseUrl, "POST");
    string jsonUser = JsonifyUser(user, token, 0);
    byte[] bodyRaw = Encoding.UTF8.GetBytes(jsonUser);

    req.uploadHandler = (UploadHandler) new UploadHandlerRaw(bodyRaw);
    req.downloadHandler = (DownloadHandler) new DownloadHandlerBuffer();
    req.SetRequestHeader("Content-Type", "application/json");

    yield return req.Send();

    jsonUser = req.downloadHandler.text;
    HighScore h = JsonUtility.FromJson<HighScore>(jsonUser);
    Debug.Log("Username: " + h.user);
    
    PlayerPrefs.SetString("username", h.user);
    PlayerPrefs.SetString("token", h.token);
    PlayerPrefs.SetInt("highScore", h.score);

    Debug.Log("------\nSaatu data:" + jsonUser);
    Debug.Log("Status code: " + req.responseCode);
  }

  private IEnumerator SendScore(string user, string token, int score) {
    UnityWebRequest req = new UnityWebRequest(baseUrl, "PUT");
    string jsonUser = JsonifyUser(user, token,  score);
    byte[] bodyRaw = Encoding.UTF8.GetBytes(jsonUser);

    req.uploadHandler = (UploadHandler) new UploadHandlerRaw(bodyRaw);
    req.downloadHandler = (DownloadHandler) new DownloadHandlerBuffer();
    req.SetRequestHeader("Content-Type", "application/json");

    yield return req.Send();



    Debug.Log("Status code: " + req.responseCode);
  }

  private string JsonifyUser(string user, string token,  int score) {
    HighScore h = new HighScore();
    h.user = user;
    h.token = token;
    h.score = score;
    return JsonUtility.ToJson(h);
  }

  [System.Serializable]
  private class HighScore{
    public string _id; 
    public string user;
    public string token;
    public int score;
  }
}
