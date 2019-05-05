using UnityEngine;
using Image = UnityEngine.UI.Image;

public class DamageVisuals : MonoBehaviour {
    
    public Sprite damage;
    
    
    public void DamageVisual(){
        GetComponent<Image>().sprite = damage;
    }

}
