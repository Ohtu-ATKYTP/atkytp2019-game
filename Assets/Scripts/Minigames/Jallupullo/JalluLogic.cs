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
    public JalluDifficultyAdjuster difAdjuster;
    public bool gameOver;
    [Tooltip("Probability of healthy drink that must be drunk")]
    public float healthyProb = .7f;


    protected override void Start() {
        base.Start();
        finishText.gameObject.SetActive(false);
        gameOver = false;
        JalluState.isHealthy = Random.Range(0f, 1f) < healthyProb;
        StartCoroutine(CORIntroduceGame());
    }

    protected override Task DisplayEndingActions(bool won) {
        finishText.gameObject.SetActive(true);
        string messageToPlayer;
        if (JalluState.isHealthy.Value) {
            messageToPlayer = won ? "Nautit raikkaan juoman!" : "Jano yltyi liian suureksi";
        } else {
            messageToPlayer = won ? "Tarkkaavainen valinta!" : "Ei kannata juoda kaikkea löytämäänsä...";
        }
        finishText.text = messageToPlayer;
        return base.DisplayEndingActions(won);
    }


    public override void OnTimerEnd() {
        if (JalluState.isHealthy.Value) {
            LoseMinigame();
        } else {
            WinMinigame();
        }
    }

    protected override void EndMinigameAsync(bool won) {
        if (gameOver) {
            return;
        }
        gameOver = true;
        base.EndMinigameAsync(won);
    }

    protected override void ConfigureDifficulty(int difficulty) {
        difAdjuster.Configure(difficulty);
    }

    public void AddLiquid() {
        remainingLiquid++;
    }

    public void RemoveLiquid() {
        remainingLiquid--;
        if (remainingLiquid <= 0) {
            if (JalluState.isHealthy.Value) {
                this.WinMinigame();
            } else {
                this.LoseMinigame(); 
            }
        }
    }

    public IEnumerator CORIntroduceGame() {
        while (introductionLength > 0) {
            introductionLength -= Time.deltaTime;
            yield return null;
        }

        if (!Application.isEditor) {
            FindObjectOfType<JalluRotator>().Initialize();
        }
    }


}

public static class JalluState
{
    public static bool? isHealthy = null;

}
