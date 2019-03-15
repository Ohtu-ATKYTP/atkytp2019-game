using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Image = UnityEngine.UI.Image;
using UnityEngine.UI;

public class JumpmanLogic : MonoBehaviour {
    
    public Sprite Face;
    bool firstJump;

    void start() {
        firstJump = false;
    }

    public void Jump() {
        if(!firstJump){
            firstJump = true;
            Text startText = GameObject.FindGameObjectWithTag("InfoText").GetComponent<Text>();
            startText.text = "";
        }

        GetComponent<Rigidbody2D>().gravityScale = Random.Range(30,200);
        //GetComponent<Rigidbody2D>().mass = Random.Range(1,10);
        GetComponent<Rigidbody2D>().velocity = Vector2.zero; //Zero velocity before jump
        GetComponent<Rigidbody2D>().AddForce(Vector2.up*3000, ForceMode2D.Impulse);
         
    }
    
    public void ChangeFace(){
        GetComponent<Image>().sprite = Face;
        GetComponent<Image>().color = Color.white;
    }
}