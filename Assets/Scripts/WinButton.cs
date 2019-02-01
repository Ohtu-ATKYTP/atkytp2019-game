using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class WinButton : MonoBehaviour
{
    private DataController dataController;
    private void Start() {
        dataController = FindObjectOfType<DataController>();
    }
    // Start is called before the first frame update
    public void LoadLevelMenu(string level)
    {
        //level = filterOdd(level);
        //SceneManager.LoadScene(level);
        dataController.SetRoundEndStatus(true);
        int randint = UnityEngine.Random.Range(0,2);
        dataController.SetWintStatus(randint == 1);
    }

    private string filterOdd(string str) {
        char[] arr = str.ToCharArray();

        arr = Array.FindAll<char>(arr, (c => (char.IsLetterOrDigit(c) 
                                  || char.IsWhiteSpace(c) 
                                  || c == '-')));
        str = new string(arr);
        return str;
    }
}
