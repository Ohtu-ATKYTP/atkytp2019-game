using UnityEngine;
using UnityEngine.UI;

public class HaalariUpdater : MonoBehaviour {
    private Image haalariImage;
    public Sprite tkoalyHaalari;
    public Sprite asteriskiHaalari;
    public Sprite luuppiHaalari;
    public Sprite serveriHaalari;
    public Sprite linkkiHaalari;
    public Sprite blankoHaalari;

    void Start () {
        haalariImage = GetComponent<Image> ();
    }
    public void ChangeImage (int haalari) {
        switch (haalari) {
            case 0:
                haalariImage.sprite = tkoalyHaalari;
                break;
            case 1:
                haalariImage.sprite = asteriskiHaalari;
                break;
            case 2:
                haalariImage.sprite = luuppiHaalari;
                break;
            case 3:
                haalariImage.sprite = serveriHaalari;
                break;
            case 4:
                haalariImage.sprite = linkkiHaalari;
                break;
            case 5:
                haalariImage.sprite = blankoHaalari;
                break;
            default:
                haalariImage.sprite = tkoalyHaalari;
                break;
        }
    }
}