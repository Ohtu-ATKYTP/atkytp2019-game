using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HaalariUpdater : MonoBehaviour {
    public Sprite tkoalyHaalari;
    public Sprite asteriskiHaalari;
    public void changeImage (int haalari) {
        switch (haalari) {
            case 0:
                GetComponent<Image> ().sprite = tkoalyHaalari;
                break;
            case 1:
                GetComponent<Image> ().sprite = asteriskiHaalari;
                break;
            default:
                GetComponent<Image> ().sprite = tkoalyHaalari;
                break;
        }
    }
}