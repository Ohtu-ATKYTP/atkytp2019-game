using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class FadingInstructor : MonoBehaviour
{
    public float visibleDuration;
    public float fadeDuration;
    public float delay = 0;
    private Text[] texts;
    private SpriteRenderer[] sprites;

    void Start() {
        Initialize();
    }

    void Initialize() {
        texts = GetComponentsInChildren<Text>();
        sprites = GetComponentsInChildren<SpriteRenderer>();
        UpdateTextsAlpha(0);
        UpdateSpritesAlpha(0);
    }

    public void Fade(float visibleTime, float transitionTime) {
        Fade(visibleTime, transitionTime, 0);

    }

    public void Fade(float visibleTime, float transitionTime, float delay) {
        if (texts == null && sprites == null) {
            Initialize();
        }
        this.delay = delay;
        this.visibleDuration = visibleTime;
        this.fadeDuration = transitionTime;
        if (sprites == null) {
            StartCoroutine(CORFadeAwayText());
        } else {
            StartCoroutine(CORFadeAway());
        }

    }


    private Color ChangeAlpha(Color c, float a) {
        return new Color(c.r, c.g, c.b, a);
    }

    private void UpdateTextsAlpha(float a) {
        texts.ToList().ForEach(txt => {
            txt.color = ChangeAlpha(txt.color, a);
            txt.SetAllDirty();
        });
    }


    private void UpdateSpritesAlpha(float a) {
        sprites.ToList().ForEach(sprite =>
                sprite.color = ChangeAlpha(sprite.color, a));
    }

    private IEnumerator CORFadeAway() {

        while (delay > 0) {
            yield return null;
            delay -= Time.unscaledDeltaTime;
        }


        UpdateTextsAlpha(1);
        UpdateSpritesAlpha(1);
        while (visibleDuration >= 0) {
            yield return null;
            visibleDuration -= Time.unscaledDeltaTime;
        }


        float t = 1f;
        do {
            yield return null;
            t -= Time.unscaledDeltaTime / fadeDuration;
            UpdateTextsAlpha(t);
            UpdateSpritesAlpha(t);
        } while (t >= 0);
    }



    private IEnumerator CORFadeAwayText() {

        while (delay > 0) {
            yield return null;
            delay -= Time.unscaledDeltaTime;
        }

        UpdateTextsAlpha(1);
        while (visibleDuration >= 0) {
            yield return null;
            visibleDuration -= Time.unscaledDeltaTime;
        }


        float t = 1f;
        do {
            yield return null;
            t -= Time.unscaledDeltaTime / fadeDuration;
            UpdateTextsAlpha(t);
        } while (t >= 0);
    }

}
