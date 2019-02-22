using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HaalariUpdater : MonoBehaviour {
    public Sprite tkoalyHaalari;
    public Sprite asteriskiHaalari;
    public Sprite luuppiHaalari;
    public Sprite serveriHaalari;
    public Sprite linkkiHaalari;
    public Sprite blankoHaalari;
    public void changeImage (int haalari) {
        switch (haalari) {
            case 0:
                GetComponent<Image> ().sprite = tkoalyHaalari;
                break;
            case 1:
                GetComponent<Image> ().sprite = asteriskiHaalari;
                break;
            case 2:
                GetComponent<Image> ().sprite = luuppiHaalari;
                break;
            case 3:
                GetComponent<Image> ().sprite = serveriHaalari;
                break;
            case 4:
                GetComponent<Image> ().sprite = linkkiHaalari;
                break;
            case 5:
                GetComponent<Image> ().sprite = blankoHaalari;
                break;
            default:
                GetComponent<Image> ().sprite = tkoalyHaalari;
                break;
        }
    }
}