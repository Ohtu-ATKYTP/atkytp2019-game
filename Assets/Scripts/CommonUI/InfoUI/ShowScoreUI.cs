using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class ShowScoreUI : MonoBehaviour
{
    public Text Score;
    DataController dataController;
    // Start is called before the first frame update
    void Start()
    {
        dataController = FindObjectOfType<DataController>();
        if(dataController != null)
        {
            this.Score.text = dataController.GetCurrentScore().ToString();
        }
        else
        {
            this.Score.text = "-";
        }

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
