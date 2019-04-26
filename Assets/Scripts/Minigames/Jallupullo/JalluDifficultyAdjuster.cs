using System.Collections;
using System.Collections.Generic;
using UnityEngine;



/*
 * Mitäs vaikeutta tässä voisi olla?
 *  - lyhyempi aika
 *  - eri asiat jotka kertovat mitä pullossa on
 *  - flashing efektejä tai kameran huojuntaa
 *  - 
 */
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
        if (overrideDifficulty) {
            difficulty = forcedDifficulty;
        }

        timer.SetTime(Mathf.Max(10f - difficulty, 4.5f));

    }
}
