using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElevatorRescueSetup : MonoBehaviour {
    //Responsible for setting up the scene based on difficulty
    //Descriptions based on difficulty levels:
    //1: 1 dude, 10 seconds
    //2: 1 dude, slightly shorter time
    //3: 1 dude, 1 obstacle, 10 seconds
    //4: 1 dude, 1 obstacle, shorter time
    //5: 1 dude, 2 obstacles, 10 seconds
    //6: 2 dudes, 1 obstacle, 10 seconds
    //7: 2 dudes, 2 obstacles, 10 seconds
    //8: 2 dudes, 2 obstacles, shorter time
    //9: 2 dudes, 2 obstacles, even shorter time
    //10+: More obstacles and shorter time
    // Start is called before the first frame update
 
    public GameObject dude1;
    public GameObject dude2;
    public GameObject obstacle1;
    public GameObject obstacle2;
    public GameObject obstacle3;


    public void Setup(int difficulty) {
        SetupTimer(difficulty);
        SetupDudes(difficulty);
        SetupObstacles(difficulty);
    }

    private void SetupTimer(int difficulty) {
        TimeProgress timer = FindObjectOfType<TimeProgress>();
        float time;
        //input the number of seconds you want to have per level
        switch (difficulty) {
            case 1:
                time = 10f;
                break;
            case 2:
                time = 8.5f;
                break;
            case 3:
                time = 10f;
                break;
            case 4:
                time = 8.5f;
                break;
            case 5:
                time = 10f;
                break;
            case 6:
                time = 10f;
                break;
            case 7:
                time = 10f;
                break;
            case 8:
                time = 8.5f;
                break;
            case 9:
                time = 7.5f;
                break;
            case 10:
                time = 6f;
                break;
            default:
                time = 5f;
                break;
        }
        timer.SetTime(time);
    }

    private void SetupDudes(int difficulty) {
        if (difficulty < 6 && difficulty > 0) {
            DisableRandomDude();
        } 
    }

    private void SetupObstacles(int difficulty) {
        if (difficulty > 0 && difficulty < 10) {
            obstacle3.SetActive(false);
            if (difficulty < 5 || difficulty == 6) {
                obstacle2.SetActive(false);
                if (difficulty < 3) {
                    obstacle1.SetActive(false);
                }
            }
        } 
    }

    private void DisableRandomDude() {
        if (Random.value < .5f) {
            dude1.SetActive(false);
        } else {
            dude2.SetActive(false);
        }
    }
}
