using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Image = UnityEngine.UI.Image;
using UnityEngine.UI;

public class JumpmanLogic : MonoBehaviour {
    
    public Sprite jumping;
    public Sprite standing;
    public Sprite scared;

    private ElevatorGameLogic EGLogic;
    private JumperPositions jumperPositions;
    private GameObject heightLine;
    private Rigidbody2D RB;
    
    private bool firstJump;
    private bool highEnough;
    private bool inJump;

    private float androidScaler;

    private float jumpForceStart;
    private float minJumpForce;
    private float maxJumpForce;
    private float downForce;
    private float gravMultiplier;
    private float jumpMultiplier;
    private float gravScaleStart;
    private float gravScale;


    void Start() {

        //Physics parameters. Adjust for different behaviour.
        gravScaleStart = 200;
        jumpForceStart =  5000;
        gravMultiplier = 40;
        jumpMultiplier = 300;
        downForce = 300000;

        //If physics behave differently on project and android, adjust this multiplier
        androidScaler = 1f;

        highEnough = false;
        firstJump = false;
        inJump = false;
        
        EGLogic = GameObject.FindGameObjectWithTag("Logic").GetComponent<ElevatorGameLogic>();
        jumperPositions = GameObject.FindGameObjectWithTag("Logic").GetComponent<JumperPositions>();
        heightLine = GameObject.Find("HeightLine");
        RB = GetComponent<Rigidbody2D>();

        if(DataController.GetDebugMode()){
            this.initDebuggerParams();
        }

        this.AdjustDifficulty(DataController.GetDifficulty());
    }

    void Update(){
        this.CheckYpos();
    }


    //If debugger screen is used, you can adjust parameters with sliders.
    private void initDebuggerParams(){
        gravScaleStart = DataController.getGameParameter("gravityStart");
        gravMultiplier = DataController.getGameParameter("gravityMultiplier");
        jumpMultiplier = DataController.getGameParameter("jumpMultiplier");
    }
    
    //Adjusts jumpforce and gravity scale based on difficulty;
    public void AdjustDifficulty(float difficulty){
        gravScale = (gravScaleStart+((difficulty-1)*gravMultiplier))*androidScaler;
        minJumpForce = (jumpForceStart + ((difficulty-1)*jumpMultiplier))*androidScaler;
        maxJumpForce = minJumpForce*2f;
    }
    
    private void CheckYpos(){
        if(transform.position.y > heightLine.transform.position.y){
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

    public void Jump() {
        if(!firstJump){
            firstJump = true;
            GameObject infoText = GameObject.Find("InfoText");
            if(infoText != null) {
               GameObject.Find("InfoText").SetActive(false);
            }
        }
        if(!inJump){
            GetComponent<Image>().sprite = jumping;
        }
        inJump = true;
        RB.gravityScale = gravScale;
        RB.velocity = Vector2.zero;
        RB.AddForce(Vector2.up*Random.Range(minJumpForce, maxJumpForce), ForceMode2D.Impulse);
    }
    
    public void ChangeToScared(){
        GetComponent<Image>().sprite = scared;
    }

    void OnCollisionEnter2D(Collision2D collision2D){
        if(collision2D.gameObject.name == "BottomBorder") {
            GetComponent<Image>().sprite = standing;
            inJump = false;
        }
    }

    public void ForceDown(){
        GetComponent<Rigidbody2D>().AddForce(Vector2.down*downForce, ForceMode2D.Impulse);
    }
}