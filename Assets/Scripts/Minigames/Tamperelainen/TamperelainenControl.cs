using UnityEngine;
using UnityEngine.UI;
public class TamperelainenControl : MonoBehaviour

{
    private Rigidbody2D rb;

    private float drunkSpeed = 1f;
    private float direction = 0f;

    //finals

    private readonly float SPEED = 7000f;
    private readonly float DRUNK_SPEED_RANGE = 5000f;
    private readonly float START_TIME = 1.0f;
    private readonly float INTERVAL = 1.5f;

    private bool isFalling;

    public Image broken_window;

    Animator animator;

    // Start is called before the first frame update

    private TamperelainenLogic logicScript;
    void Start()
    {
        animator = GameObject.Find("GameObjects").GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        InvokeRepeating("DefineDrunkDirectionAndSpeed", START_TIME, INTERVAL);
        logicScript = FindObjectOfType<TamperelainenLogic>();
        broken_window.enabled = false;
        isFalling = false;
        animator.SetBool("isFalling", isFalling);
    }


    void FixedUpdate()
    {

        
        Control();
        Drunkify();

    }

    /// <summary>
    /// OnCollisionEnter is called when this collider/rigidbody has begun
    /// touching another rigidbody/collider.
    /// </summary>
    /// <param name="other">The Collision data associated with this collision.</param>
    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.name == "Hitbox_left")
        {
            BreakWindow();
            this.enabled = false;
            logicScript.LoseMinigame();
        }
        else if (other.gameObject.name == "Hitbox_right")
        {
            isFalling = true;
            animator.SetBool("isFalling", isFalling);
            this.enabled = false;
            logicScript.LoseMinigame();
            
        }
    }

    private void Control()
    {
        rb.AddForce(transform.right * Input.acceleration.x * SPEED * (1 + (Mathf.Abs(GetPositionX()) / 100)));
    }

    private void Drunkify()
    {
        rb.AddForce(transform.right * this.direction * this.drunkSpeed);
    }

    private void DefineDrunkDirectionAndSpeed()
    {
        this.direction = Random.Range(-DRUNK_SPEED_RANGE, DRUNK_SPEED_RANGE);
        this.drunkSpeed = Random.Range(1f, 2f);

    }



    private float GetPositionX()
    {
        float position_x = this.GetComponent<RectTransform>().position.x;
        return position_x;
    }

    private void FallRight()
    {
        //Destroy(GetComponent<HingeJoint2D>());
    }

    private void BreakWindow()
    {
        broken_window.enabled = true;
        Destroy(this.gameObject);
    }


}
