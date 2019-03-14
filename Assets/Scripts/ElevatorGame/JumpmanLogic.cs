using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Image = UnityEngine.UI.Image;

public class JumpmanLogic : MonoBehaviour {
    
    public Sprite Face;

    void Update(){

        //if (Input.GetMouseButtonDown(0)) {
        //    this.Jump();
        //}

        //if (Input.touchCount > 0) {
        //    this.Jump();
        //}
    }

    public void Jump() {
        GetComponent<Rigidbody2D>().velocity = Vector2.zero; //Zero velocity before jump
        GetComponent<Rigidbody2D>().AddForce(Vector2.up*3000, ForceMode2D.Impulse);
    }
    
    public void ChangeFace(){
        GetComponent<Image>().sprite = Face;
        GetComponent<Image>().color = Color.white;
    }
}