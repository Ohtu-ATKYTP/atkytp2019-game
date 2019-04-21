using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JalluLogic : MinigameLogic
{
    [Tooltip("Time in seconds for intro text and before physics/rotation apply")]
    public float introductionLength;
    private int remainingLiquid = 0; 
   

    protected override void Start() {
        base.Start();
        StartCoroutine(CORIntroduceGame());
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
