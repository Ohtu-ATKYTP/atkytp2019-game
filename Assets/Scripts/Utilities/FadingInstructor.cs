using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class FadingInstructor : MonoBehaviour {

    public float fadeDuration = 5f;
    private Text[] texts;
    private SpriteRenderer[] sprites;

    void Start() {
        texts = GetComponents<Text>();
        sprites = GetComponents<SpriteRenderer>();
        StartCoroutine(CORFadeAway());
    }

    private IEnumerator CORFadeAway() {
        float t = 0;
        Color[] startingTextColors = texts.Select(txt => txt.color).ToArray();
        Color[] goalTextColors = startingTextColors.Select(c => new Color(c.r, c.g, c.b, 100)).ToArray();
        Color[] startingSpriteColors = sprites.Select(sprt => sprt.color).ToArray();
        Color[] goalSpriteColors = startingSpriteColors.Select(c => new Color(c.r, c.g, c.b, 100)).ToArray();


        do {
            yield return null;
            t += Time.deltaTime / fadeDuration;
            for (int i = 0; i < texts.Length; i++) {
                texts[i].color = Color.Lerp(startingTextColors[i], goalTextColors[i], t);
            }
            for (int i = 0; i < sprites.Length; i++) {
                sprites[i].color = Color.Lerp(startingSpriteColors[i], goalSpriteColors[i], t);
            }
        } while (t <= 1);



    }

}
