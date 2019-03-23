using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Image = UnityEngine.UI.Image;
using UnityEngine.UI;

public class JumpmanLogic : MonoBehaviour {
    
    public Sprite jumping;
    public Sprite standing;
    public Sprite scared;
    public bool gameWon;
    
    bool firstJump;
    
    private DataController dataController;

    private float maxGravScale;
    private float minGravScale;
    private float jumpForce;

    void Start() {
        firstJump = false;
        gameWon = false;

        dataController = FindObjectOfType<DataController>();
        maxGravScale = 1000;
        minGravScale = 600;
        jumpForce = 13000;

        if(dataController.GetDebugMode()){
            this.initDebuggerParams();
        }
    }

    private void initDebuggerParams(){
        maxGravScale = dataController.getGameParameter("gravityScaleMax");
        minGravScale = dataController.getGameParameter("gravityScaleMin");
        jumpForce = dataController.getGameParameter("jumpForce");
    }

    public void Jump() {
        if(!firstJump){
            firstJump = true;
            GameObject.FindGameObjectWithTag("InfoText").SetActive(false);
        }
        GetComponent<Image>().sprite = jumping;
        GetComponent<Rigidbody2D>().gravityScale = Random.Range(minGravScale,maxGravScale);
        GetComponent<Rigidbody2D>().velocity = Vector2.zero; //Zero velocity before jump
        GetComponent<Rigidbody2D>().AddForce(Vector2.up*jumpForce, ForceMode2D.Impulse);
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