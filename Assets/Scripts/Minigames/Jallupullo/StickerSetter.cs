using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityAsyncAwaitUtil;

public class StickerSetter : MonoBehaviour
{
    public Stickers stickerOptions;
    private SpriteRenderer renderer; 
    async void Start() {
        renderer = GetComponent<SpriteRenderer>(); 
        await new WaitUntil(() => JalluState.isHealthy != null);
        Configure(JalluState.isHealthy.Value);
    }

    private void Configure(bool healthy) {
        Sprite sticker = stickerOptions.RandomSticker(healthy);
        renderer.sprite = sticker;
    }
}
