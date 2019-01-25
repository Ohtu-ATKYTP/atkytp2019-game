using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class WinButton : MonoBehaviour
{
    // Start is called before the first frame update
    public void LoadLevelMenu()
    {
        SceneManager.LoadScene("Assets/Scenes/MainMenu.unity");
    }
}
