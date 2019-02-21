using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using System.Timers;    
using UnityEngine;

public class TimeProgress : MonoBehaviour
{
    // Start is called before the first frame update
    public int seconds;
    private float timer;
    public Slider timerSlider;

    public UnityEngine.Events.UnityEvent TimerReadyMethods;
    private DataController dataController;
    private bool timerEnded = false;

    void Start()
    {
        this.timer = (float) seconds;
        timerSlider.value = 1f;
        dataController = FindObjectOfType<DataController>();
    }

    // Update is called once per frame
    void Update()
    {
        this.timer -= Time.deltaTime;
        timerSlider.value = this.timer /seconds;
        if (this.timer <= 0.0f && this.timerEnded == false) {
            this.timerEnded = true;
            timerEnd();
        }


    }

    private void timerEnd() {
        TimerReadyMethods.Invoke();
    }



}
