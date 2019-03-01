using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloodAnimation : MonoBehaviour
{
    public Transform flood;
    public ParticleSystem bubbles;
    public GameObject pointer;
    public GameObject topSprite;
    private Vector3 direction = new Vector3(0, 1, 0);
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
            direction *= 1 + (2 * Time.deltaTime);
            flood.Translate(direction * Time.deltaTime);
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
