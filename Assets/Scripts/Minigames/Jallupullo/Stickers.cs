using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "StickerOptions", menuName = "ATK-YTP/Jallupullo", order = 1)]
public class Stickers : ScriptableObject
{
    public Sprite[] healthyStickers;
    public Sprite[] deadlyStickers;
    private System.Random rndm;


    private void Initialize(){ 
        rndm = new System.Random(); 
   }

    public Sprite RandomSticker(bool healthy) {
        if(rndm == null){ 
            Initialize(); 
        }
        int len = healthy ? healthyStickers.Length : deadlyStickers.Length;
        int idx = rndm.Next(0, len);
        return healthy ?  healthyStickers[idx] : deadlyStickers[idx];
    }

}
