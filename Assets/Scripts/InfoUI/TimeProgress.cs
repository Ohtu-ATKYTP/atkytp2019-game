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
    private bool timerEnded = false;

    void Start() {
        this.timer = seconds;
        timerSlider.value = 1f;
        dataController = FindObjectOfType<DataController>();
    }

    // Update is called once per frame
    void Update() {
        if(this.timerEnded){
                return;
            }

        this.timer -= Time.deltaTime;
        timerSlider.value = this.timer / seconds;
        if (this.timer <= 0.0f && this.timerEnded == false) {
            this.timerEnded = true;
            timerEnd();
        }


    }

    
    public void StopTimerProgression() {
        timerEnded = true;
    }

    private void timerEnd() {
        TimerReadyMethods.Invoke();
    }

    public void SetTime(float time) {
        this.timer = time;
        this.seconds = time;
    }

}
