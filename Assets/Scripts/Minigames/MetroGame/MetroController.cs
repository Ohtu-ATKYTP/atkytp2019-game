using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MetroController : MonoBehaviour, IMinigameEnder
{
    public Transform metroPos;
    public Transform scene;
    public BoxCollider2D w1;
    public BoxCollider2D w2;
    public Rigidbody2D rb;
    bool gameOver = false;
    
    void Start(){
        scene.Rotate(0,0,Mathf.Max(-90,(DataController.GetDifficulty()-1)*-10));
        rb.gravityScale = Mathf.Pow(1.0f+Mathf.Max(0f,(DataController.GetDifficulty()-5)/10.0f),2);
    }

    public void WinMinigame() {
        this.gameOver = true;
        FindObjectOfType<TimeProgress>().StopTimerProgression();
        GameManager.endMinigame(true);
    }
    
    public void LoseMinigame() {
        gameOver = true;
        FindObjectOfType<TimeProgress>().StopTimerProgression();
        GameManager.endMinigame(false);
    }

    void Update()
    {
        if(w1.IsTouching(w2) && !gameOver){
            WinMinigame();
        }
    }
}
