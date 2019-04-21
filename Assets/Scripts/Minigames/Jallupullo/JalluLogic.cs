using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JalluLogic : MinigameLogic
{
    [Tooltip("Time in seconds for intro text and before physics/rotation apply")]
    public float introductionLength; 
   

    protected override void Start() {
        base.Start();
        StartCoroutine(CORIntroduceGame());
    }
    protected override void DisplayEndingActions(bool won) {
        string message = won ? "You won!" : "You lost :/";
        Debug.Log(message);
    }

    protected override void ConfigureDifficulty(int difficulty) {
        Debug.Log("The difficulty of the Jallu shall be: " + difficulty);
        FindObjectOfType<TimeProgress>().SetTime(1000f);
    }

    public IEnumerator CORIntroduceGame() {
        while(introductionLength > 0){ 
            introductionLength -= Time.deltaTime;
            yield return null; 
            }

        FindObjectOfType<JalluRotator>().Initialize(); 
    }
}
