using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JalluLogic : MinigameLogic
{
    protected override void DisplayEndingActions(bool won) {
        string message = won ? "You won!" : "You lost :/";
        Debug.Log(message);
    }

    protected override void ConfigureDifficulty(int difficulty) {
        Debug.Log("The difficulty of the Jallu shall be: " + difficulty);
    }
}
