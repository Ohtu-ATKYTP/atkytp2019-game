using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using System.Timers;
using UnityEngine;

public class TimeProgress : MonoBehaviour {
    // Start is called before the first frame update
    public float seconds;
    private float timer;
    public Slider timerSlider;

    public UnityEngine.Events.UnityEvent TimerReadyMethods;
    private DataController dataController;
    private bool timerPaused = false;

    void Start() {
        this.timer = seconds;
        timerSlider.value = 1f;
        dataController = FindObjectOfType<DataController>();
    }

    // Update is called once per frame
    void Update() {
        if (this.timerPaused) {
            return;
        }

        this.timer -= Time.deltaTime;
        timerSlider.value = this.timer / seconds;
        if (this.timer <= 0.0f && this.timerPaused == false) {
            this.timerPaused = true;
            timerEnd();
        }
    }


    public void TogglePause() {
        timerPaused = !timerPaused;
    }

    public void StopTimerProgression() {
        timerPaused = true;
    }

    private void timerEnd() {
        TimerReadyMethods.Invoke();
    }



}
