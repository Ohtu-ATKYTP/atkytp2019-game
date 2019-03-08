using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class WinButton : MonoBehaviour, IMinigameEnder {
    private DataController dataController;
    private void Start() {
        dataController = FindObjectOfType<DataController>();
    }

    public void LoseMinigame() {
        dataController.MinigameEnd(false, 0);
    }

    public void WinMinigame() {
        dataController.MinigameEnd(true, 10);
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
