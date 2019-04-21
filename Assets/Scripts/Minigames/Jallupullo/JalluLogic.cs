using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

public class JalluLogic : MinigameLogic
{
    [Tooltip("Time in seconds for intro text and before physics/rotation apply")]
    public float introductionLength;
    private int remainingLiquid = 0;
    public Text finishText;

   

    protected override void Start() {
        base.Start();
        finishText.gameObject.SetActive(false);
        StartCoroutine(CORIntroduceGame());
    }

    protected override Task DisplayEndingActions(bool won)
    {
        finishText.gameObject.SetActive(true);
        finishText.text = won ? "You drank the whole bottle" : "You showed restraint";
        return base.DisplayEndingActions(won);
    }

    protected override void ConfigureDifficulty(int difficulty) {
        Debug.Log("The difficulty of the Jallu shall be: " + difficulty);
        FindObjectOfType<TimeProgress>().SetTime(1000f);
    }

    public void AddLiquid()
    {
        remainingLiquid++; 
    }

    public void RemoveLiquid()
    {
        remainingLiquid--; 
        if (remainingLiquid <= 0)
        {
            this.WinMinigame();
        }
    }

    

    public IEnumerator CORIntroduceGame() {
        while(introductionLength > 0){ 
            introductionLength -= Time.deltaTime;
            yield return null; 
            }

        FindObjectOfType<JalluRotator>().Initialize(); 
    }
}
