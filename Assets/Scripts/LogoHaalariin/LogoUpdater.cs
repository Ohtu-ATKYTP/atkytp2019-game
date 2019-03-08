using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LogoUpdater : MonoBehaviour {
    private float successRotation = 360;
    private RectTransform logoTransform;
    private Image logoImage;
    private bool dropLogoAnimation = false;
    private bool rotateLogoAnimation = false;
    public Sprite tkoalyLogo;
    public Sprite asteriskiLogo;
    public Sprite luuppiLogo;
    public Sprite serveriLogo;
    public Sprite linkkiLogo;
    public Sprite blankoLogo;

    void Start () {
        logoTransform = GetComponent<RectTransform> ();
        logoImage = GetComponent<Image> ();
    }
    public void changeImage (int logo) {
        switch (logo) {
            case 0:
                logoImage.sprite = tkoalyLogo;
                break;
            case 1:
                logoImage.sprite = asteriskiLogo;
                break;
            case 2:
                logoImage.sprite = luuppiLogo;
                break;
            case 3:
                logoImage.sprite = serveriLogo;
                break;
            case 4:
                logoImage.sprite = linkkiLogo;
                break;
            case 5:
                logoImage.sprite = blankoLogo;
                break;
            default:
                logoImage.sprite = tkoalyLogo;
                break;
        }
    }
    public void startDropLogoAnimation () {
        this.dropLogoAnimation = true;
    }
    public void startRotateLogoAnimation () {
        this.rotateLogoAnimation = true;
    }
    void Update () {
        if (dropLogoAnimation) {
            logoTransform.localPosition += Vector3.down * Time.deltaTime * 1000;
            logoTransform.Rotate (Vector3.right * Time.deltaTime * 1000);
            if (logoTransform.position.y < -1000) dropLogoAnimation = false;
        }

        if (rotateLogoAnimation) {
            float rotation = 400 * Time.deltaTime;
            if (this.successRotation > rotation) {
                this.successRotation -= rotation;
                transform.Rotate (0, rotation, 0);
            } else {
                rotateLogoAnimation = false;
            }
        }
    }
}