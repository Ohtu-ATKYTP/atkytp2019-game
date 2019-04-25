using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class TamperelainenControl : MonoBehaviour

{
    private Rigidbody2D rb;

    private float drunkSpeed = 1f;
    private float direction = 0f;

    //finals
    private readonly float ACCEPTED_AREA_LEFT = 70f;
    private readonly float ACCEPTED_AREA_RIGHT = 300f;
    private readonly float SPEED = 7000f;
    private readonly float DRUNK_SPEED_RANGE = 5000f;
    private readonly float START_TIME = 1.0f;
    private readonly float INTERVAL = 1.5f;

    public Image broken_window;

    // Start is called before the first frame update

    private TamperelainenLogic logicScript;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        InvokeRepeating("DefineDrunkDirectionAndSpeed", START_TIME, INTERVAL);
        logicScript = FindObjectOfType<TamperelainenLogic>();
        broken_window.enabled = false;
    }


    void FixedUpdate()
    {
        Control();
        Drunkify();
        //CheckLoseCondition();


    }

    /// <summary>
    /// OnCollisionEnter is called when this collider/rigidbody has begun
    /// touching another rigidbody/collider.
    /// </summary>
    /// <param name="other">The Collision data associated with this collision.</param>
    void OnCollisionEnter2D(Collision2D other)
    {
        Debug.Log("Hit!");
        if (other.gameObject.name == "Hitbox_left") {
            BreakWindow();
            this.enabled = false;
            logicScript.LoseMinigame();
        } else if (other.gameObject.name == "Hitbox_right") {
            this.enabled = false;
            logicScript.LoseMinigame();
        }
    }

    private void Control() {
        rb.AddForce(transform.right * Input.acceleration.x * SPEED * (1 + (Mathf.Abs(GetPositionX()) / 100)));
    }

    private void Drunkify() {
        rb.AddForce(transform.right * this.direction * this.drunkSpeed);
    }

    private void DefineDrunkDirectionAndSpeed() {
        this.direction = Random.Range(-DRUNK_SPEED_RANGE, DRUNK_SPEED_RANGE);
        this.drunkSpeed = Random.Range(1f, 2f);

    }

    private void CheckLoseCondition() {
        float xPostion = this.GetPositionX();
        Debug.Log("Position x:" + xPostion);
        if (xPostion < ACCEPTED_AREA_LEFT || xPostion > ACCEPTED_AREA_RIGHT ) {
            if (xPostion < ACCEPTED_AREA_LEFT) BreakWindow(); else FallRight();
            this.enabled = false;
            logicScript.LoseMinigame();
            
        }
    }

    private float GetPositionX() {
        float position_x = this.GetComponent<RectTransform>().position.x;
        return position_x;
    }

    private void FallRight() {
        //Destroy(GetComponent<HingeJoint2D>());
    }

    private void BreakWindow() {
        broken_window.enabled = true;
    }


}
