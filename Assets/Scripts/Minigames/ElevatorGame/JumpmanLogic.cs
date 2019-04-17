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

    private float maxGravScale;
    private float minGravScale;
    private float minJumpForce;
    private float maxJumpForce;
    private float downForce;

    public float height;
    private bool highEnough;

    private ElevatorGameLogic EGLogic;
    private JumperPositions jumperPositions;

    void Start() {

        highEnough = false;

        firstJump = false;
        gameWon = false;

        EGLogic = GameObject.FindGameObjectWithTag("Logic").GetComponent<ElevatorGameLogic>();

        jumperPositions = GameObject.FindGameObjectWithTag("Logic").GetComponent<JumperPositions>();

        maxGravScale = 20; //1000;
        minGravScale = 10; //600
        minJumpForce = 1000; //13000
        maxJumpForce = 5000;

        downForce = 7000;

        //if(DataController.GetDebugMode()){
        //    this.initDebuggerParams();
        //}
    }

    void Update(){
        this.CheckYpos();
    }

    public void SetGravScale(float gravScaleAdjuster){
        maxGravScale = maxGravScale*gravScaleAdjuster;
        minGravScale = minGravScale*gravScaleAdjuster;
    }

    private void CheckYpos(){
        if(transform.position.y > 350){
            if(highEnough == false){
                highEnough = true;
                jumperPositions.increaseJumpmenHighEnough();
                GetComponent<Image>().color = Color.red;
            }
            
        }else{
            if(highEnough == true){
                highEnough = false;
                GetComponent<Image>().color = Color.white;
                jumperPositions.decreaseJumpmenHighEnough();
            }
            
        }
    }

    private void initDebuggerParams(){
        maxGravScale = DataController.getGameParameter("gravityScaleMax");
        minGravScale = DataController.getGameParameter("gravityScaleMin");
        maxJumpForce = DataController.getGameParameter("jumpForce");
    }

    public void Jump() {
        if(!firstJump){
            firstJump = true;
            GameObject infoText = GameObject.FindGameObjectWithTag("InfoText");
            if(infoText != null) {
               GameObject.FindGameObjectWithTag("InfoText").SetActive(false);
            }
        }
        GetComponent<Image>().sprite = jumping;
        GetComponent<Rigidbody2D>().gravityScale = Random.Range(minGravScale,maxGravScale);
        GetComponent<Rigidbody2D>().velocity = Vector2.zero; //Zero velocity before jump
        GetComponent<Rigidbody2D>().AddForce(Vector2.up*Random.Range(minJumpForce, maxJumpForce), ForceMode2D.Impulse);
    }
    
    public void ChangeToScared(){
        GetComponent<Image>().sprite = scared;
    }

    void OnCollisionEnter2D(Collision2D collision2D){
        if(collision2D.gameObject.name == "BottomBorder" && !gameWon) {
            GetComponent<Image>().sprite = standing;
        }
    }

    public void ForceDown(){
        GetComponent<Rigidbody2D>().AddForce(Vector2.down*downForce, ForceMode2D.Impulse);
    }

}