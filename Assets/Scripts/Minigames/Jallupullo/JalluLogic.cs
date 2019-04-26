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


    protected override void Start() {
        base.Start();
        finishText.gameObject.SetActive(false);
        gameOver = false;
        StartCoroutine(CORIntroduceGame());
    }

    protected override Task DisplayEndingActions(bool won) {
        finishText.gameObject.SetActive(true);
        finishText.text = won ? "You drank a bottle of tasty refreshment!" : "You wasted too much time";
        return base.DisplayEndingActions(won);
    }


    protected override  void EndMinigameAsync(bool won) {
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
            this.WinMinigame();
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
