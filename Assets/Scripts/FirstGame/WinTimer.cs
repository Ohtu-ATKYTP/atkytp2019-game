using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using System.Timers;    
using UnityEngine;

public class WinTimer : MonoBehaviour
{
    // Start is called before the first frame update
    public int seconds;
    private float timer;
    public Scrollbar timerSlider;
    private DataController dataController;
    private bool timerEnded = false;
    void Start()
    {
        this.timer = (float) seconds;
        timerSlider.value = this.timer;
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
        dataController.MinigameEnd(false, 0);
    }


}
