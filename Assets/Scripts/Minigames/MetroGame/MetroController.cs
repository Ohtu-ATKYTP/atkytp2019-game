using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MetroController : MonoBehaviour, IMinigameEnder
{
    public Transform metroPos;
    public Transform scene;
    public BoxCollider2D w1;
    public BoxCollider2D w2;
    bool gameOver = false;
    
//DataController.GetDifficulty()
    void Start(){
        scene.Rotate(0,0,Mathf.Max(-90,(DataController.GetDifficulty()-1)*-5));
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
        if(w1.IsTouching(w2) && !gameOver){
            WinMinigame();
        }
    }
}
