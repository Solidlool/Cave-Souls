using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    public CharacterController2D controller;
    public Animator animator;
    public CharacterController2D notMoving;
    public CharacterController2D Sliding;

    private float vertical;
    private float climbingSpeed = 8f;
    private bool isLadder;
    private bool isClimbing;
    [SerializeField] private Rigidbody2D rb;

    public PlayerCombat combat;
    float horizontalMove = 0f;

    private PlayerStats PlayerStats;

    public float runSpeed=40f;

    bool jump = false;
    bool crouch = false;

    public PlayerStats staminaLoss;
    public float currentStamina;
    public float JumpStaminaCost = 60f;


    public float JumpCooldown = 0.3f;
    private float timeSinceJump = 0f;



    public bool gliding = false;
    private float heldTimer;
    public float glidTimer = 0.3f;

    
    

    public bool roll = false;


    private void Start()
    {
        staminaLoss = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerStats>();
        PlayerStats = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerStats>();

    }



    // Start is called before the first frame update


    // Update is called once per frame
    void Update()
    {

        timeSinceJump += Time.deltaTime;

        currentStamina = PlayerStats.currentStamina;

        vertical = Input.GetAxis("Vertical");

        if(isLadder&& Mathf.Abs(vertical)> 0f)
        {
            isClimbing = true;
            animator.SetBool("isClimbing", true);
        }

        animator.SetFloat("climbingSpeed", Mathf.Abs(vertical));

        if (!combat.isBlocking)
        {

            horizontalMove = Input.GetAxisRaw("Horizontal") * runSpeed;

            animator.SetFloat("Speed", Mathf.Abs(horizontalMove));
        }

 
    
    if (Input.GetButton("Jump") && currentStamina >= JumpStaminaCost && timeSinceJump>JumpCooldown && !jump ) 


        {
            timeSinceJump = 0;
            staminaLoss.StaminaLoss(JumpStaminaCost);
            heldTimer += Time.deltaTime;
            if(heldTimer< glidTimer) 
            
            {
               jump = true;
               animator.SetBool("IsJumping", true);
               animator.SetBool("isHooked", false);
            }
     
            if (heldTimer>=glidTimer)
            {
                gliding = true;
            }

        }
        if (Input.GetButtonUp("Jump"))
        {
            heldTimer = 0f;
            gliding = false;
              
        }
        else if (Input.GetButton("Jump"))
        {
            Debug.Log(" I cant do that right Now");
        }






        if (!isClimbing)
        {
            if (Input.GetButtonDown("Crouch"))
            {
                crouch = true;
            }
            else if (Input.GetButtonUp("Crouch"))
            {
                crouch = false;
            }
        }
    }
  
  public void OnHookEvent(bool isHooked)
    {
        animator.SetBool("IsJumping", false);
        animator.SetBool("isHooked", isHooked);
        Debug.Log("i am hooked");
    }

    public void OnLanding()
    {
        animator.SetBool("IsJumping", false);
        animator.SetBool("isSliding", false);
    }

    public void OnCrouching(bool isCrouching)
    {
        animator.SetBool("isCrouching", isCrouching);
    }

    public void OnSlideEvent(bool isSliding) 
       
        {
        animator.SetBool("IsJumping", false);
        animator.SetBool("isSliding", isSliding);

        }
   

    void FixedUpdate()
        {

        controller.Move(horizontalMove * Time.fixedDeltaTime, crouch, jump, gliding,roll);
        jump = false;

        if (isClimbing)
        {
            rb.gravityScale = 0f;
            rb.velocity = new Vector2(rb.velocity.x, vertical * climbingSpeed);

        }
        else
        {
            rb.gravityScale = 5f;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Ladder"))
        {
            isLadder = true;
            Debug.Log("im on the ladder");
 
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Ladder"))
        {
            isLadder = false;
            isClimbing = false;
            animator.SetBool("isClimbing", false);
            Debug.Log("im off the ladder");

        }
    }

}
