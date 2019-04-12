using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class ShowScoreUI : MonoBehaviour
{
    public Text Score;
    // Start is called before the first frame update
    void Start()
    {
        this.Score.text = DataController.GetCurrentScore() + "";

    }
}
