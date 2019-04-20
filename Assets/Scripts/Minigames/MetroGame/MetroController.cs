using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MetroController : MonoBehaviour, IMinigameEnder
{
    public Transform metroPos;
    public Transform scene;
    public int dif;
    bool gameOver = false;
    

    void Start(){
        scene.Rotate(0,0,Mathf.Max(-60,(dif-1)*-5));
    }

    public void WinMinigame() {
        this.gameOver = true;
        FindObjectOfType<TimeProgress>().StopTimerProgression();
        GameManager.endMinigame(true, 10);
    }
    
    public void LoseMinigame() {
        gameOver = true;
        FindObjectOfType<TimeProgress>().StopTimerProgression();
        GameManager.endMinigame(false, 0);
    }

    void Update()
    {
        if(metroPos.position.x<-6 && !gameOver){
            WinMinigame();
        }
    }
}
