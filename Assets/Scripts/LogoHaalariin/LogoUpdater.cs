using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LogoUpdater : MonoBehaviour {
    public Sprite tkoalyLogo;
    public Sprite asteriskiLogo;
    public Sprite luuppiLogo;
    public Sprite serveriLogo;
    public Sprite linkkiLogo;
    public Sprite blankoLogo;
    public void changeImage(int logo)
    {
        switch (logo) {
            case 0:
                GetComponent<Image> ().sprite = tkoalyLogo;
                break;
            case 1:
                GetComponent<Image> ().sprite = asteriskiLogo;
                break;
            case 2:
                GetComponent<Image> ().sprite = luuppiLogo;
                break;
            case 3:
                GetComponent<Image> ().sprite = serveriLogo;
                break;
            case 4:
                GetComponent<Image> ().sprite = linkkiLogo;
                break;
            case 5:
                GetComponent<Image> ().sprite = blankoLogo;
                break;
            default:
                GetComponent<Image> ().sprite = tkoalyLogo;
                break;
        }
    }
}