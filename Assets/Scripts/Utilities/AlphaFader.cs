using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; 

public class AlphaFader : MonoBehaviour
{
    public float timeBeforeFade;
    public float fadeLength;
    private Text toFade;

    void Start() {
        toFade = GetComponent<Text>(); 
        StartCoroutine(CORFadeAway());
    }

    // Update is called once per frame

    public IEnumerator CORFadeAway() {
        while (timeBeforeFade > 0) {
            timeBeforeFade -= Time.deltaTime;
            yield return null;
        }

        float t = toFade.color.a;
        Color c = new Color(toFade.color.r, toFade.color.g, toFade.color.b, toFade.color.a);
        while (t > 0) {
            c.a = t; 
            toFade.color = c;
            t -= Time.deltaTime / fadeLength;
            yield return null;

        }
    }
}
