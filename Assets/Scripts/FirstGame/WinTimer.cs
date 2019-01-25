using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using System.Timers;    
using UnityEngine;

public class WinTimer : MonoBehaviour
{
    // Start is called before the first frame update
    private float timer;
    public Scrollbar timerSlider;

    void Start()
    {
        this.timer = 10.0f;
        Debug.Log(this.timer.ToString());
        timerSlider.value = this.timer;


        
        
    }

    // Update is called once per frame
    void Update()
    {
        this.timer -= Time.deltaTime;
        timerSlider.value = this.timer /10;
        if (this.timer <= 0.0f) {
            timerEnded();
        }


    }

    private void timerEnded() {

    }


}
