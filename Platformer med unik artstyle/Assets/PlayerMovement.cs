using UnityEngine;
using System.Collections;

public class PlayerMovement : MonoBehaviour {

    private Rigidbody2D rb;
    public float speed;
    public float jumpForce;
    private float moveInput;

    private bool isGrounded;
    public Transform feetPos;
    public float checkRadius;
    public LayerMask whatIsGround;

    private float jumpTimeCounter;
    public float jumpTime;
    private bool isJumping;

    public Animator anim;

	// Use this for initialization
	void Start () {
	    rb = GetComponent<Rigidbody2D>();
	}
	
	// Update is called once per frame
	void FixedUpdate () {

        //bevæger sig hele tiden anpå moveInput
        moveInput = Input.GetAxisRaw("Horizontal");
        rb.velocity = new Vector2(moveInput * speed , rb.velocity.y);
    }

    void Update()
    {
        //isGrounded er hvis der er Ground under en
        isGrounded = Physics2D.OverlapCircle(feetPos.position, checkRadius, whatIsGround);

        //Hvis man går til højre, vend til højre | Hvis man går til venstre, vend til venstre
        if (moveInput > 0)
        {
            transform.eulerAngles = new Vector3(0, 0, 0);
        }
        else if (moveInput < 0)
        {
            transform.eulerAngles = new Vector3(0, 180, 0);
        }

        //Hvis man bevæger sig på ground -> Gå animation, ellers Idle animation
        if(isGrounded == true && moveInput != 0)
        {
            anim.SetInteger("state", 1);
        }
        else
            anim.SetInteger("state", 0);

        //Hvis man trykker hop, nede på jorden -> så hopper man + hoppe animation
        if (isGrounded == true && Input.GetButtonDown("Jump"))
        {
            isJumping = true;
            jumpTimeCounter = jumpTime;
            rb.velocity = Vector2.up * jumpForce;
            anim.SetInteger("state", 2);
        }

        //Hvis man hopper i forvejen og holder hoppe knappen nede -> så fortsætter man med at ryge op ad, til timeren ryger ud.
        if (Input.GetButton("Jump") && isJumping == true)
        {
            if (jumpTimeCounter > 0)
            {
                rb.velocity = Vector2.up * jumpForce;
                jumpTimeCounter -= Time.deltaTime;
                anim.SetInteger("state", 2);
            }
            else
            {
                isJumping = false;
            }
        }

        //Hvis man har givet slip på knappen -> så ryger man ned igen
        if (Input.GetButtonUp("Jump"))
        {
            isJumping = false;
        }

        //Hvis man er i luften og er færdig med at hoppe -> så falder man
        if(isJumping == false && isGrounded == false)
        {
            anim.SetInteger("state", 3);
        }
    }
}
