using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InfoTextScript : MonoBehaviour {
    private Text t;
    public ElevatorRescueLogic elevatorRescueLogic;
    // Start is called before the first frame update
    void Start() {
        t = GetComponent<Text>();
        t.enabled = true;
    }

    // Update is called once per frame
    void Update() {
        t.enabled = !elevatorRescueLogic.GetGameOver();
    }
}
