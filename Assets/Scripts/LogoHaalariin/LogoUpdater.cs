using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LogoUpdater : MonoBehaviour {
    public Sprite tkoalyLogo;
    public Sprite asteriskiLogo;
    public void changeImage(int logo)
    {
        switch (logo) {
            case 0:
                GetComponent<Image> ().sprite = tkoalyLogo;
                break;
            case 1:
                GetComponent<Image> ().sprite = asteriskiLogo;
                break;
            default:
                GetComponent<Image> ().sprite = tkoalyLogo;
                break;
        }
    }
}