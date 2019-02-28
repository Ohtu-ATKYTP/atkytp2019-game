using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RegisterButton : MonoBehaviour
{
    private bool isVisible; 

    void Start()
    {
        
    }

    void Update()
    {
        isVisible = PlayerPrefs.GetInt("registered") == 0 ? true: false;
        gameObject.SetActive(isVisible);
    }
}
