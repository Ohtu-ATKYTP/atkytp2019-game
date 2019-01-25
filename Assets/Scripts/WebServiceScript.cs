using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using System.Text;

public class WebServiceScript : MonoBehaviour {
  private string baseUrl = "http://atkytpgame.herokuapp.com/api/highscores";
  public string highscores;

  void Start() {
  }

  //Use this method to fetch highscores
  public void GetHighscores() {
    StartCoroutine(GetHighscoresText());
  }

  //Use this method to add new users
  public void RegisterUser(string name, string token, string installationId) {
    StartCoroutine(SendUser(name, token, installationId));
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

  private IEnumerator SendUser(string name, string token, string installationId) {
    UnityWebRequest req = new UnityWebRequest(baseUrl, "POST");
    string jsonUser = JsonifyUser(name, token, installationId, 0);
    Debug.Log(jsonUser);
    byte[] bodyRaw = Encoding.UTF8.GetBytes(jsonUser);

    req.uploadHandler = (UploadHandler) new UploadHandlerRaw(bodyRaw);
    req.downloadHandler = (DownloadHandler) new DownloadHandlerBuffer();
    req.SetRequestHeader("Content-Type", "application/json");

    yield return req.Send();

    Debug.Log("Status code: " + req.responseCode);
  }

  private string JsonifyUser(string name, string token, string installationId, int score) {
    User u = new User();
    u.name = name;
    u.token = token;
    u.installationId = installationId;
    u.score = score;
    return JsonUtility.ToJson(u);
  }

  [System.Serializable]
  private class User{
    public string name;
    public string token;
    public string installationId;
    public int score;
  }
}
