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

    private float gravMultiplier;
    private float jumpMultiplier;


    void Start() {

        //Physics parameters
        gravScaleStart = 75;
        jumpForceStart =  3000;
        gravMultiplier = 35;
        jumpMultiplier = 200;
        androidScaler = 1;
        downForce = 300000;

        highEnough = false;
        firstJump = false;
        
        EGLogic = GameObject.FindGameObjectWithTag("Logic").GetComponent<ElevatorGameLogic>();

        jumperPositions = GameObject.FindGameObjectWithTag("Logic").GetComponent<JumperPositions>();

        heightLine = GameObject.Find("HeightLine");

        minGravScale = gravScaleStart;
        maxGravScale = minGravScale;
        
        minJumpForce = jumpForceStart; //1000; //13000
        maxJumpForce = minJumpForce*4f;//5000;
        
        if(DataController.GetDebugMode()){
            this.initDebuggerParams();
        }

        int difficulty = DataController.GetDifficulty();
        float difficultyAdjuster = difficulty;
        this.AdjustDifficulty(difficultyAdjuster);
    }

    void Update(){
        this.CheckYpos();
    }

    private void initDebuggerParams(){
        androidScaler = DataController.getGameParameter("gravityScaleMax"); //android
        gravMultiplier = DataController.getGameParameter("gravityScaleMin"); //gravMult
        jumpMultiplier = DataController.getGameParameter("jumpForce");       //jumpMult
    }
    
    //Adjusts jumpforce and gravity scale based on difficulty;
    public void AdjustDifficulty(float gravScaleAdjuster){
        maxGravScale = (maxGravScale+(gravScaleAdjuster*gravMultiplier))*androidScaler;
        minGravScale = maxGravScale;
        minJumpForce = (minJumpForce + (gravScaleAdjuster*jumpMultiplier))*androidScaler;
        maxJumpForce = minJumpForce*3f;

        //Display current gravScale, debugging reasons
        //GameObject.Find("InfoText").GetComponent<Text>().text = maxGravScale.ToString();
    }

    private void CheckYpos(){
        //if(transform.position.y > 350){
        //Android positions are different so have to use heightLine Object
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