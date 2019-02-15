using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using System.IO;                                                        // The System.IO namespace contains functions related to loading and saving files

public class DataController : MonoBehaviour 
{
    private RoundData[] allRoundData;

    private int currentScore;
    private bool roundEndStatus;
    private bool winStatus;
    private int lives;
    private readonly int MAX_LIVES = 3;



    void Start()
    {
        DontDestroyOnLoad(gameObject);
        Init();

    }

    public void Init() {
        this.currentScore = 0;
        this.roundEndStatus = false;
        this.winStatus = true;
        this.lives = MAX_LIVES;
    }

    public void MinigameEnd(bool win, int score) {
        this.SetWinStatus(win);
        this.AddCurrentScore(score);
        this.SetRoundEndStatus(true);
    }

    public void TakeLife() {
        this.lives--;
    }

    public int GetLives() {
        return this.lives;
    }

    public void ResetLives() {
        this.lives = 3;
    }

    public bool GetWinStatus() {
        return this.winStatus;
    }

    public void ResetScore() {
        this.currentScore = 0;
    }

    public void SetWinStatus(bool win) {
        this.winStatus = win;
    }

    public int GetCurrentScore() {
        return this.currentScore;
    }

    public bool GetRoundEndStatus() {
        return this.roundEndStatus;
    }

    public void AddCurrentScore(int score) {
        this.currentScore += score;
    }

    public void SetRoundEndStatus(bool win) {
        this.roundEndStatus = win;
    }

}
