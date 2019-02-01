using UnityEngine;
using UnityEngine.UI;

public class UpdatableHighscore : MonoBehaviour
{
    private void Start() {
        Text scoreText = GetComponent<Text>();
        int score = (PlayerPrefs.HasKey("highScore")) ?  PlayerPrefs.GetInt("highScore") : 0;
        scoreText.text = "High score: " + score;
    }

}
