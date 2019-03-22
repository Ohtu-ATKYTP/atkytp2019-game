using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuManager : MonoBehaviour {

    private Dictionary<string, Canvas> screens = new Dictionary<string, Canvas>{};

    private string current = "Main Menu Screen";

    private void Start() {
        screens["Registration Screen"] = GameObject.Find("Registration Screen").GetComponent<Canvas>();
        screens["Main Menu Screen"] = GameObject.Find("Main Menu Screen").GetComponent<Canvas>();
        screens["High Score Screen"] = GameObject.Find("High Score Screen").GetComponent<Canvas>();
        screens["Settings Screen"] = GameObject.Find("Settings Screen").GetComponent<Canvas>();

        foreach(Canvas c in screens.Values) {
            c.enabled = false;
        }
        
        screens["Main Menu Screen"].enabled = true;
        current = "Main Menu Screen";

        if(!PlayerPrefs.HasKey("registered")){
            displayOnlyMenu("Registration Screen");
            PlayerPrefs.SetInt("registered", 0);
        }
    }

    public void displayOnlyMenu(string canvasName) {
        screens[current].enabled = false;
        screens[canvasName].enabled = true;
        current = canvasName;
    }

}
