using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloodAnimation : MonoBehaviour
{
    public Transform flood;
    public ParticleSystem bubbles;
    public GameObject pointer;
    public GameObject topSprite;
    private bool started = false;

    private void Start()
    {
        var m = bubbles.emission;
        m.enabled = false;
    }

    public void StartAnimation()
    {
        this.started = true;
        this.pointer.SetActive(false);
        this.topSprite.SetActive(false);
        StartCoroutine(StartBubbles());
    }

    void Update()
    {
        if (started)
        {
            flood.Translate((-1*flood.position.y+1) * Vector3.up * Time.deltaTime);
            if (flood.position.y > 0) this.started = false;
        }
    }

    IEnumerator StartBubbles()
    {
        yield return new WaitForSecondsRealtime(1.5f);
        var m = bubbles.emission;
        m.enabled = true;
    }
}
