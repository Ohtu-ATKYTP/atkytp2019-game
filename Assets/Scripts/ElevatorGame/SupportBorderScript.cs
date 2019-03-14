using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Image = UnityEngine.UI.Image;

public class SupportBorderScript : MonoBehaviour {
    
    public Sprite damage1;
    public Sprite damage2;
    public Sprite damage3;
    
    public void DamageVisual(int damage){
        if(damage>=20){
            return;
        }
        if(damage>=15){
            GetComponent<Image>().sprite = damage3;
            return;
        }
        if(damage>=10){
            GetComponent<Image>().sprite = damage2;
            return;
        }
        if(damage>=5){
            GetComponent<Image>().sprite = damage1;
            return;
        }

    }

}
