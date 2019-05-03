using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; 
using UnityAsyncAwaitUtil;

public class StickerSetter : MonoBehaviour
{
    public Stickers stickerOptions;
    private Image image; 
    async void Start() {
        image = GetComponent<Image>(); 
        await new WaitUntil(() => JalluState.isHealthy != null);
        Configure(JalluState.isHealthy.Value);
    }

    private void Configure(bool healthy) {
        Sprite sticker = stickerOptions.RandomSticker(healthy);
        image.sprite = sticker;
    }
}
