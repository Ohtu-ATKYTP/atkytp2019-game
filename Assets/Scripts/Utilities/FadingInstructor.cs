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
    private Image[] images;

    private bool initialized = false;

    private float[] initTextAlphas;
    private float[] initSpriteAlphas;
    private float[] initImageAlphas;


    void Start() {
        if (!initialized) {
            Initialize();
        }
    }

    void Initialize() {
        initialized = true;
        texts = GetComponentsInChildren<Text>();
        sprites = GetComponentsInChildren<SpriteRenderer>();
        images = GetComponentsInChildren<Image>();

        // Initial alpha of image / text might well be less than one - need to store for when that element is displayed again
        initTextAlphas = texts.Select(t => t.color.a).ToArray();
        initSpriteAlphas = sprites.Select(s => s.color.a).ToArray();
        initImageAlphas = images.Select(i => i.color.a).ToArray();

        UpdateTextsAlpha(0);
        UpdateSpritesAlpha(0);
        UpdateImagesAlpha(0);
    }

    public void Fade(float visibleTime, float transitionTime) {
        Fade(visibleTime, transitionTime, 0);

    }

    public void Fade(float visibleTime, float transitionTime, float delay) {

        if (!initialized) {
            Initialize();
        }
        this.delay = delay;
        this.visibleDuration = visibleTime;
        this.fadeDuration = transitionTime;
        StartCoroutine(CORFadeAway());
    }


    private Color ChangeAlpha(Color c, float a) {
        return new Color(c.r, c.g, c.b, a);
    }

    private void UpdateTextsAlpha(float a) {
        texts.ToList().ForEach(txt => {
            txt.color = ChangeAlpha(txt.color, Mathf.Min(a, txt.color.a));
            txt.SetAllDirty();
        });
    }


    private void UpdateSpritesAlpha(float a) {
        sprites.ToList().ForEach(sprite =>
                sprite.color = ChangeAlpha(sprite.color, Mathf.Min(a, sprite.color.a)));
    }

    private void UpdateImagesAlpha(float a) {
        images.ToList().ForEach(image =>
        image.color = ChangeAlpha(image.color, Mathf.Min(a, image.color.a)));
    }

    private IEnumerator CORFadeAway() {

        // we need to wait for a frame always before updating the initial alpha, otherwise the instructions remain invisible
        if (delay <= 0) {
            yield return null;
        }

        while (delay > 0) {
            yield return null;
            delay -= Time.unscaledDeltaTime;
        }




        for (int i = 0; i < texts.Length; i++) {
            texts[i].color = ChangeAlpha(texts[i].color, initTextAlphas[i]);
        }

        for (int i = 0; i < sprites.Length; i++) {
            sprites[i].color = ChangeAlpha(sprites[i].color, initSpriteAlphas[i]);
        }

        for (int i = 0; i < images.Length; i++) {
            images[i].color = ChangeAlpha(images[i].color, initImageAlphas[i]);
        }

        while (visibleDuration > 0) {
            yield return null;
            visibleDuration -= Time.unscaledDeltaTime;

        }

        float t = 1f;


        do {
            yield return null;
            t -= Time.unscaledDeltaTime / fadeDuration;
            UpdateTextsAlpha(t);
            UpdateSpritesAlpha(t);
            UpdateImagesAlpha(t);
        } while (t > 0);
    }




}
