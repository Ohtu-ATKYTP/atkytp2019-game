using UnityEngine;
using UnityEngine.UI;

public class OwnHighScoreText : MonoBehaviour
{
    private Text scoreText;
    private void Start() {
        this.scoreText = GetComponent<Text>();
        int score = (PlayerPrefs.HasKey("highScore")) ?  PlayerPrefs.GetInt("highScore") : 0;
        scoreText.text = "High score: " + score;
    }

    private void Update() {
        int score = (PlayerPrefs.HasKey("highScore")) ?  PlayerPrefs.GetInt("highScore") : 0;
        scoreText.text = "High score: " + score;
    }
}
