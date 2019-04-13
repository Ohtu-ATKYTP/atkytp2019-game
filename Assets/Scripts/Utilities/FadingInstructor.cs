using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class FadingInstructor : MonoBehaviour {

    public float fadeDuration = 1f;
    private Text[] texts;
    private SpriteRenderer[] sprites;

    void Start() {
        texts = GetComponentsInChildren<Text>();
        sprites = GetComponentsInChildren<SpriteRenderer>();
        StartCoroutine(CORFadeAway());
    }



    private IEnumerator CORFadeAway() {
        float t = 1f;



        do {
            yield return null;
            t -= Time.deltaTime / fadeDuration;
            for (int i = 0; i < texts.Length; i++) {
                texts[i].color = new Color(texts[i].color.r, texts[i].color.g, texts[i].color.b, t);
                texts[i].SetAllDirty();
            }
            for (int i = 0; i < sprites.Length; i++) {
                sprites[i].color = new Color(sprites[i].color.r, sprites[i].color.g, sprites[i].color.b, t);
            }
            Debug.Log("T: " + t);
            Debug.Log("Counts: " + texts.Length + sprites.Length);
        } while (t >= 0);
        Debug.Log("DONE");


    }

}
