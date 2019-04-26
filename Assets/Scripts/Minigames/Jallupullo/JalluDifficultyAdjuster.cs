using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JalluDifficultyAdjuster : MonoBehaviour
{


    private bool initialized = false;

    void Start() {
        if (!initialized) {
            Initialize();
        }
    }

    private void Initialize() {
        initialized = true;

    }

    public void Configure(int difficulty) {

    }

    // Update is called once per frame
    void Update() {

    }
}
