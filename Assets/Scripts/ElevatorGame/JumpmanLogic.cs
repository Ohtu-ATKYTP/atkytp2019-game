using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Image = UnityEngine.UI.Image;
using UnityEngine.UI;

public class JumpmanLogic : MonoBehaviour {
    
    public Sprite jumping;
    public Sprite standing;
    public Sprite scared;
    bool firstJump;
    public bool gameWon;

    void start() {
        firstJump = false;
        gameWon = false;
    }

    public void Jump() {
        if(!firstJump){
            firstJump = true;
            Text startText = GameObject.FindGameObjectWithTag("InfoText").GetComponent<Text>();
            startText.text = "";
        }
        GetComponent<Image>().sprite = jumping;
        GetComponent<Rigidbody2D>().gravityScale = Random.Range(30,200);
        //GetComponent<Rigidbody2D>().mass = Random.Range(1,10);
        GetComponent<Rigidbody2D>().velocity = Vector2.zero; //Zero velocity before jump
        GetComponent<Rigidbody2D>().AddForce(Vector2.up*3000, ForceMode2D.Impulse);
    }
    
    public void ChangeToScared(){
        GetComponent<Image>().sprite = scared;
    }

    void OnCollisionEnter2D(Collision2D collision2D){
        if(collision2D.gameObject.name == "BottomBorder" && !gameWon) {
            GetComponent<Image>().sprite = standing;
        }
    }

}