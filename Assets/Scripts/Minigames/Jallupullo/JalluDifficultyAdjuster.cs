using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JalluDifficultyAdjuster : MonoBehaviour
{

    private TimeProgress timer;
    private bool initialized = false;

    public bool overrideDifficulty = false;
    public int forcedDifficulty;

    void Start() {
        if (!initialized) {
            Initialize();
        }
    }

    private void Initialize() {
        initialized = true;
        timer = FindObjectOfType<TimeProgress>();
    }

    public void Configure(int difficulty) {
        if (!initialized) {
            Initialize();
        }
        if (overrideDifficulty) {
            difficulty = forcedDifficulty;
        }

        timer.SetTime(Mathf.Max(12f - difficulty * 2f, 4.5f));

    }
}
