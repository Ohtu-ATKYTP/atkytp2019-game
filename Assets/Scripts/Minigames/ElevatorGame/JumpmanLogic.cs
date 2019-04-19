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
    
    private float maxGravScale;
    private float minGravScale;
    private float minJumpForce;
    private float maxJumpForce;
    private float downForce;

    bool firstJump;
    public float height;
    private bool highEnough;

    private float androidScaler;
    private float gravScaleStart;
    private float jumpForceStart;


    void Start() {
        gravScaleStart = 1000;
        jumpForceStart = 40000;
        androidScaler = 50;

        if(DataController.GetDebugMode()){
            this.initDebuggerParams();
        }

        highEnough = false;
        firstJump = false;
        
        EGLogic = GameObject.FindGameObjectWithTag("Logic").GetComponent<ElevatorGameLogic>();

        jumperPositions = GameObject.FindGameObjectWithTag("Logic").GetComponent<JumperPositions>();

        heightLine = GameObject.Find("HeightLine");

        


        minGravScale = gravScaleStart* Time.fixedDeltaTime * androidScaler; //10; //600
        maxGravScale = minGravScale; //20; //100;
        

        minJumpForce = jumpForceStart* Time.fixedDeltaTime * androidScaler; //1000; //13000
        maxJumpForce = minJumpForce*2f;//5000;
        
        downForce = 300000* Time.fixedDeltaTime * androidScaler;

        
    }

    void Update(){
        this.CheckYpos();
    }

    private void initDebuggerParams(){
        androidScaler = DataController.getGameParameter("gravityScaleMax"); //android
        gravScaleStart = DataController.getGameParameter("gravityScaleMin");
        jumpForceStart = DataController.getGameParameter("jumpForce");
    }

    public void setAndroidScaler(float androidScale){
        androidScaler = androidScale; 
    }

    public void SetGravScale(float gravScaleAdjuster){
        maxGravScale = maxGravScale*gravScaleAdjuster*0.8f;
        minGravScale = minGravScale*gravScaleAdjuster*0.8f;
        minJumpForce = minJumpForce + (10000*gravScaleAdjuster*Time.fixedDeltaTime*androidScaler);
        maxJumpForce = minJumpForce*2f;
    }

    private void CheckYpos(){
        //if(transform.position.y > 350){
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
        GetComponent<Image>().sprite = jumping;
        GetComponent<Rigidbody2D>().gravityScale = Random.Range(minGravScale,maxGravScale);
        GetComponent<Rigidbody2D>().velocity = Vector2.zero; //Zero velocity before jump
        GetComponent<Rigidbody2D>().AddForce(Vector2.up*Random.Range(minJumpForce, maxJumpForce), ForceMode2D.Impulse);
    }
    
    public void ChangeToScared(){
        GetComponent<Image>().sprite = scared;
    }

    void OnCollisionEnter2D(Collision2D collision2D){
        if(collision2D.gameObject.name == "BottomBorder") {
            GetComponent<Image>().sprite = standing;
        }
    }

    public void ForceDown(){
        GetComponent<Rigidbody2D>().AddForce(Vector2.down*downForce, ForceMode2D.Impulse);
    }

}