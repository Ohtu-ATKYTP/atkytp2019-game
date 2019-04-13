using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Image = UnityEngine.UI.Image;

public class SupportBorderScript : MonoBehaviour {
    
    public Sprite damage1;
    public Sprite damage2;
    public Sprite damage3;
    
    public void DamageVisual(float damage){
        if(damage>=4){
            return;
        }
        if(damage>=3){
            GetComponent<Image>().sprite = damage3;
            return;
        }
        if(damage>=2){
            GetComponent<Image>().sprite = damage2;
            return;
        }
        if(damage>=1){
            GetComponent<Image>().sprite = damage1;
            return;
        }

    }

}
