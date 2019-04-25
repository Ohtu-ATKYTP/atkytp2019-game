using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LogoUpdater : MonoBehaviour {
    private float successRotation = 360;
    private float logoFadeTime = 0f;
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
        logoImage = GetComponent<Image> ();

        initializeFadeTime();
    }

    private void initializeFadeTime() {
        int difficulty = DataController.GetDifficulty();
        if (difficulty <= 3) {
            logoFadeTime = 0f;
        } else if (difficulty <= 6) {
            logoFadeTime = 2f;
        } else if (difficulty <= 9) {
            logoFadeTime = 1f;
        } else if (difficulty <= 12) {
            logoFadeTime = 0.5f;
        } else {
            logoFadeTime = 0.25f;
        }
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
        startfadeLogoAnimation();
    }

    public void startDropLogoAnimation () {
        setLogoVisible();
        this.dropLogoAnimation = true;
    }

    public void startRotateLogoAnimation () {
        setLogoVisible();
        this.rotateLogoAnimation = true;
    }

    private void startfadeLogoAnimation () {
        setLogoVisible();
        if (this.logoFadeTime > 0f) {
            logoImage.CrossFadeAlpha(0, logoFadeTime, false);
        }
    }

    private void setLogoVisible() {
        logoImage.CrossFadeAlpha(1, 0, false);
    }

    public void setLogoFadeTime(float time) {
        this.logoFadeTime = time;
    }
    void Update () {
        if (dropLogoAnimation) {
            transform.localPosition += Vector3.down * Time.deltaTime * 1000;
            transform.Rotate (Vector3.right * Time.deltaTime * 1000);
            if (transform.position.y < -1000) dropLogoAnimation = false;
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