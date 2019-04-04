using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TamperelainenControl : MonoBehaviour

{
    private Rigidbody2D rb;

    private float drunkSpeed = 1f;
    private float direction = 0f;

    //finals
    private readonly float ACCEPTED_AREA = 60f;
    private readonly float SPEED = 200f;
    private readonly float DRUNK_SPEED_RANGE = 50f;
    private readonly float CIRCLE = 360f;
    // Start is called before the first frame update

    private TamperelainenLogic logicScript;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        InvokeRepeating("DefineDrunkDirectionAndSpeed", 1.0f, 0.7f);
        logicScript = FindObjectOfType<TamperelainenLogic>();
    }


    void FixedUpdate()
    {
        Control();
        Drunkify();
        CheckLoseCondition();

    }

    private void Control() {
        rb.AddForce(transform.right * Input.acceleration.x * SPEED * (1 + (GetRelativeRotation() / 100)));
    }

    private void Drunkify() {
        rb.AddForce(transform.right * this.direction * this.drunkSpeed);
    }

    private void DefineDrunkDirectionAndSpeed() {
        this.direction = Random.Range(-DRUNK_SPEED_RANGE, DRUNK_SPEED_RANGE);
        this.drunkSpeed = Random.Range(1f, 2f);

    }

    private void CheckLoseCondition() {
        float zRotation = this.GetRelativeRotation();
        if (zRotation < -ACCEPTED_AREA || zRotation > ACCEPTED_AREA ) {
            if (zRotation > ACCEPTED_AREA) FallRight(); else BreakWindow();
            logicScript.LoseMinigame();
            this.enabled = false;
        }
    }

    private float GetRelativeRotation() {
        float zRotation = this.GetComponent<RectTransform>().eulerAngles.z;
        if (zRotation > CIRCLE/2) {
            zRotation = zRotation - CIRCLE;
        }
        return zRotation;
    }

    private void FallRight() {
        //this.GetComponent<HingeJoint>();
    }

    private void BreakWindow() {

    }


}
