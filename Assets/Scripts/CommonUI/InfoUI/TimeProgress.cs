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
    private bool timerPaused = false;
    // after stopping the pause status cannot be toggled: the timer will run any longer in the minigame
    private bool timerStopped = false;

    void Start() {
        this.timer = seconds;
        timerSlider.value = 1f;
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
        if (!timerStopped) {
            timerPaused = !timerPaused;
        }
    }

    public void StopTimerProgression() {
        timerPaused = true; 
        timerStopped = true;
    }

    private void timerEnd() {
        TimerReadyMethods.Invoke();
    }

    public void SetTime(float time) {
        this.timer = time;
        this.seconds = time;
    }

}
