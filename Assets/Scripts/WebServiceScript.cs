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
  public void getHighscores() {
    StartCoroutine(getHighscoresText());
  }

  //Use this method to add new users
  public void registerUser(string name, string token) {
    StartCoroutine(sendUser(name, token));
  }

  private IEnumerator getHighscoresText() {
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

  private IEnumerator sendUser(string name, string token) {
    UnityWebRequest req = new UnityWebRequest(baseUrl, "POST");
    string jsonUser = jsonifyUser(name, token);
    Debug.Log(jsonUser);
    byte[] bodyRaw = Encoding.UTF8.GetBytes(jsonUser);

    req.uploadHandler = (UploadHandler) new UploadHandlerRaw(bodyRaw);
    req.downloadHandler = (DownloadHandler) new DownloadHandlerBuffer();
    req.SetRequestHeader("Content-Type", "application/json");

    yield return req.Send();

    Debug.Log("Status code: " + req.responseCode);
  }

  private string jsonifyUser(string name, string token) {
    User u = new User();
    u.name = name;
    u.token = token;
    u.score = 0;
    return JsonUtility.ToJson(u);
  }

  [System.Serializable]
  private class User{
    public string name;
    public string token;
    public int score;
  }
}
