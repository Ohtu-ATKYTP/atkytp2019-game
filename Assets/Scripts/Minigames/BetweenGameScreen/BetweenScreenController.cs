using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BetweenScreenController : MonoBehaviour {
    private float startTime;
    public Text score;
    public Image h1;
    public Image h2;
    public Image h3;
    public Text winStatus;
    private bool active;
    private Image lostHeart;
    private Vector3 startScale;
    private int increasingScore;
    private int scoreDifference;
    private float lastUpdate;
    // Start is called before the first frame update
    void Start() {
        active = true;
        startTime = Time.time;
        h2.enabled = DataController.GetLives() > 0;
        h3.enabled = DataController.GetLives() > 1;
        increasingScore = DataController.GetCurrentScore() - DataController.GetLastReceivedScore();
        scoreDifference = DataController.GetLastReceivedScore();
        if (DataController.GetWinStatus()) {
            winStatus.text = "WIN";
        } else {
            winStatus.text = "LOSS";
        }
        score.text = "" + increasingScore;
        lastUpdate = Time.time;

        if (DataController.GetLives() == 0) {
            h1.enabled = !DataController.GetWinStatus();
            h2.enabled = false;
            h3.enabled = false;
            lostHeart = h1;
        } else if (DataController.GetLives() == 1) {
            h2.enabled = !DataController.GetWinStatus();
            h3.enabled = false;
            lostHeart = h2;
        } else if (DataController.GetLives() == 2) {
            h3.enabled = !DataController.GetWinStatus();
            lostHeart = h3;
        } else {
            lostHeart = null;
        }
        if (DataController.GetWinStatus()) {
            lostHeart = null;
        } else {
            startScale = lostHeart.transform.localScale;

        }
    }

    // Update is called once per frame
    void Update() {
        if (lostHeart != null) {
            lostHeart.transform.localScale = lostHeart.transform.localScale - startScale * Time.deltaTime;
            if (lostHeart.transform.localScale.x <= 0) {
                lostHeart.transform.localScale = new Vector3(0, 0, 0);
                lostHeart = null;
            }
        }
        if (scoreDifference != 0 && (Time.time - lastUpdate) >= 1f / Mathf.Abs(scoreDifference)) {
            if (scoreDifference > 0) {
                increasingScore++;
            } else {
                increasingScore--;
            }
            score.text = "" + increasingScore;
            lastUpdate = Time.time;
            if (increasingScore == DataController.GetCurrentScore()) {
                scoreDifference = 0;
            }
        }
        if (Time.time - startTime > 3 && active) {
            active = false;
        }
    }

}