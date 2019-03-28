using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BetweenScreenController : MonoBehaviour
{
	private DataController dataController;
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
			dataController = FindObjectOfType<DataController>();
			startTime = Time.time;
			h2.enabled = dataController.GetLives() > 0;
			h3.enabled = dataController.GetLives() > 1;
			increasingScore = dataController.GetCurrentScore()-dataController.GetLastReceivedScore();
			scoreDifference = dataController.GetLastReceivedScore();
			if(dataController.GetWinStatus()){
				winStatus.text = "WIN";
			}else{
				winStatus.text = "LOSS";
			}
			score.text = "" + increasingScore;
			lastUpdate = Time.time;
			if(dataController.GetLives()==0){
				h2.enabled = false;
				h3.enabled = false;
				lostHeart = h1;
			}else if(dataController.GetLives()==1){
				h3.enabled = false;
				lostHeart = h2;
			}else if(dataController.GetLives()==2){
				lostHeart = h3;
			}else{
				lostHeart=null;
			}
			if(dataController.GetWinStatus()){
				lostHeart = null;
			}
			startScale = lostHeart.transform.localScale;
    }

    // Update is called once per frame
    void Update() {
			if(lostHeart!=null){
				lostHeart.transform.localScale = lostHeart.transform.localScale - startScale * Time.deltaTime;
				if(lostHeart.transform.localScale.x <= 0){
					lostHeart.transform.localScale = new Vector3(0,0,0);
					lostHeart = null;
				}
			}
			if(scoreDifference > 0 && (Time.time - lastUpdate) >= 1f/scoreDifference){
				increasingScore++;
				score.text = "" + increasingScore;
				lastUpdate=Time.time;
				if(increasingScore == dataController.GetCurrentScore()){
					scoreDifference = 0;
				}
			}
			if (Time.time - startTime > 3 && active) {
				active = false;
				dataController.BetweenScreenEnd();
			}
    }

}