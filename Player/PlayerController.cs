using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private float movementInputDirection;
    private float jumpTimer;
    private float turnTimer;
    private float wallJumpTimer;
    private float dashTimeLeft;
    private float lastDash = -100f;
    private float knockbackStartTime;
    [SerializeField]
    private float knockbackDuration;
    private float hookTimer;
    public float hookLimit;
    public GameObject menu;
    
   
    public float boostForce;
    private bool canBoost = true;
   
    


    private int amountOfJumpsLeft;
    private int facingDirection = 1;
    private int lastWallJumpDirection;

    public bool isFacingRight = true;
    private bool isWalking;
    public bool isGrounded;
    private bool isHooked;
    private bool isTouchingHook;
    private bool isTouchingWall;
    private bool isTouchingBooster;
    private bool isWallSliding;
    private bool canNormalJump;
    private bool canWallJump;
    private bool canRopeJump;
    private bool isAttemptingToJump;
    private bool checkJumpMultiplier;
    public bool canMove;
    private bool canFlip;
    private bool hasWallJumped;
    private bool canHook = true;
    private bool isTouchingBackWall;
    private bool isTouchingLadder;
   
    
    private bool isClimbing;
    private bool isCrouching;
    private bool isDashing;
    private bool knockback;
    




    [SerializeField]
    private Vector2 knockbackSpeed;

    private PlayerCombat combat;
    private PlayerStats PlayerStats;
    private PlayerStats staminaLoss;
    private Rigidbody2D rb;
    private Animator anim;
    public float jumpStaminaCost;

    public int amountOfJumps = 1;

    public float ladderCheckRadius;
    private float verticalMovement;
    public float climbingSpeed = 8f;
    public float movementSpeed = 10.0f;
    public float jumpForce = 16.0f;
    public float groundCheckRadius;
    public float hookCheckRadius;
    public float boosterCheckRadius;
    public float wallBackCheckRadius;
    public float wallCheckDistance;
    public float wallSlideSpeed;
    public float movementForceInAir;
    public float airDragMultiplier = 0.95f;
    public float variableJumpHeightMultiplier = 0.5f;
    public float wallHopForce;
    public float wallJumpForce;
    public float jumpTimerSet = 0.15f;
    public float turnTimerSet = 0.1f;
    public float wallJumpTimerSet = 0.5f;
    public float airTimerLimit = 0.1f;

    public float fallMultiplier = 2.5f;
    public float lowJumpMultiplier = 2f;
    
    public float dashTime;
    public float dashSpeed;
    public float dashCoolDown;
    private float currentStamina;

    public Vector2 wallHopDirection;
    public Vector2 wallJumpDirection;

    public Transform groundCheck;
    public Transform wallCheck;
    public Transform hookCheck;
    public Transform boosterCheck;
    public Transform backWallCheck;
    public Transform ladderCheck;
    
    
    

    public LayerMask whatIsGround;
    public LayerMask whatIsHook;
    public LayerMask whatIsBooster;
    public LayerMask whatisBackWall;
    public LayerMask whatisLadder;


    private bool DashUnlocked;
    private bool ParryUnlocked;

    

    

    

    // Start is called before the first frame update
    void Start()
    {
        combat = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerCombat>();
        staminaLoss = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerStats>();
        PlayerStats = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerStats>();
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        amountOfJumpsLeft = amountOfJumps;
        wallHopDirection.Normalize();
        wallJumpDirection.Normalize();
    }

    // Update is called once per frame
    void Update()
    {
        
        CheckInput();
        CheckMovementDirection();
        UpdateAnimations();
        CheckIfCanJump();
        CheckIfWallSliding();
        GetBoostStatus();
        CheckIfWallIsBack();
        CheckClimbing();
        CheckJump();
        CheckHanging();
        CheckDash();
        CheckKnockback();
       
        CheckUnlocks();


    }

    private void FixedUpdate()
    {
        ApplyMovement();
        CheckSurroundings();
    }

    private void CheckUnlocks()
    {
        DashUnlocked = GameObject.FindGameObjectWithTag("Manager").GetComponent<GameSystem>().dashUnlocked;
        ParryUnlocked = GameObject.FindGameObjectWithTag("Manager").GetComponent<GameSystem>().parryUnlocked;
    }

    private void CheckIfWallSliding()
    {
        if (isTouchingWall  && rb.velocity.y < 0 )
        {
            isWallSliding = true;
        }
        else
        {
            isWallSliding = false;
        }
    }

    private void CheckIfWallIsBack()
    {
        if(isTouchingBackWall && wallJumpTimer > 0.1 && hasWallJumped)
        {
            Flip();
            hasWallJumped = false;
   
        }
    }

    public void GetBoostStatus()
    {

        if (hookTimer > hookLimit && isTouchingBooster)
        {
            canBoost = true;
        }
        else
        {
            canBoost = false;
        }
    }

    public bool GetDashStatus()
    {
        return isDashing;
    }

   

    public void Knockback(int direction)
    {
        knockback = true;
        knockbackStartTime = Time.time;
        rb.velocity = new Vector2(knockbackSpeed.x * direction, knockbackSpeed.y);
    }

    private void CheckKnockback()
    {
        if (Time.time >= knockbackStartTime + knockbackDuration && knockback)
        {
            knockback = false;
            rb.velocity = new Vector2(0.0f, rb.velocity.y);
        }
    }

    private void CheckSurroundings()
    {
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, whatIsGround);
        isTouchingWall = Physics2D.Raycast(wallCheck.position, transform.right, wallCheckDistance, whatIsGround);
        isTouchingHook = Physics2D.OverlapCircle(hookCheck.position, hookCheckRadius, whatIsHook);
        isTouchingBooster = Physics2D.OverlapCircle(boosterCheck.position, boosterCheckRadius, whatIsBooster);
        isTouchingBackWall = Physics2D.OverlapCircle(backWallCheck.position, wallBackCheckRadius, whatisBackWall);
        isTouchingLadder = Physics2D.OverlapCircle(ladderCheck.position, ladderCheckRadius, whatisLadder);

    }

    private void CheckIfCanJump()
    {
        currentStamina = PlayerStats.currentStamina;
        if (isGrounded && rb.velocity.y <= 0.01f || isHooked)
        {
            amountOfJumpsLeft = amountOfJumps;
        }

        if (isTouchingWall)
        {
            checkJumpMultiplier = false;
            canWallJump = true;
        }

        if (amountOfJumpsLeft <= 0)
        {
            canNormalJump = false;
            
        }

        else
        {
            canNormalJump = true;
        }

        if (isTouchingLadder)
        {
            canRopeJump = true;
        }

    }

    private void CheckMovementDirection()
    {
        if (isFacingRight && movementInputDirection < 0)
        {
            Flip();
        }
        else if (!isFacingRight && movementInputDirection > 0)
        {
            Flip();
        }

        if (Mathf.Abs(rb.velocity.x) >= 0.01f)
        {
            isWalking = true;
        }
        else
        {
            isWalking = false;
        }
    }


    private void CheckClimbing()
    {
        anim.SetFloat("climbingSpeed", Mathf.Abs(verticalMovement));


        if (isTouchingLadder && canHook && !isGrounded)
        {
            isHooked = true;
           
            rb.constraints = RigidbodyConstraints2D.FreezeAll;
            anim.SetBool("isClimbing", true);
            isClimbing = true;

        }

        if (isTouchingLadder && (verticalMovement) > 0f || isTouchingLadder && (verticalMovement) < 0f)
        {
            rb.constraints = RigidbodyConstraints2D.FreezeRotation;
            isClimbing = true;
            anim.SetBool("isClimbing", true);
            
        }
        if (!isTouchingLadder || isGrounded && (verticalMovement) < 0f)
        {
            rb.constraints = RigidbodyConstraints2D.FreezeRotation;
            isClimbing = false;
            anim.SetBool("isClimbing", false);
        }

        


    }


    private void UpdateAnimations()
    {
        anim.SetBool("isWalking", isWalking);
        anim.SetBool("isGrounded", isGrounded);
        anim.SetFloat("yVelocity", rb.velocity.y);
        anim.SetBool("isSliding", isWallSliding);
        anim.SetBool("isHooked", isHooked);
        anim.SetBool("isDashing", isDashing);
        anim.SetBool("isCrouching", isCrouching);
    }

    public void CheckInput()
    {
        movementInputDirection = Input.GetAxisRaw("Horizontal");
        verticalMovement = Input.GetAxisRaw("Vertical");

        if (Input.GetButtonDown("Jump"))
        {
            if (isGrounded || (amountOfJumpsLeft > 0 && !isTouchingWall))
            {
                NormalJump();
                
            }
            else
            {
                jumpTimer = jumpTimerSet;
                isAttemptingToJump = true;
            }
            if (isHooked )
            {
                hookTimer = 0f;
                isHooked = false;
                isTouchingLadder = false;
                
                rb.constraints = RigidbodyConstraints2D.FreezeRotation;
                NormalJump();
            }

            if (isTouchingLadder)
            {
                
                hookTimer = 0f;
                isTouchingLadder = false;
                canHook = false;

                rb.constraints = RigidbodyConstraints2D.FreezeRotation;
                NormalJump();
            }
            if (canBoost)
            {
                hookTimer = 0f;
                BoostJump();
            }

          
        }

        if (Input.GetButtonDown("Horizontal") && isTouchingWall)
        {
            if (!isGrounded && movementInputDirection == facingDirection)
            {
               
                canMove = false;
                canFlip = false;

                turnTimer = turnTimerSet;
            }
        }

        if (turnTimer >= 0)
        {
            turnTimer -= Time.deltaTime;

            if (turnTimer <= 0)
            {
                canMove = true;
                canFlip = true;
            }
        }

        if (checkJumpMultiplier && !Input.GetButton("Jump"))
        {
            checkJumpMultiplier = false;
            rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * variableJumpHeightMultiplier);
        }

        if (Input.GetButtonDown("Dash") && DashUnlocked)
        {
            if (Time.time >= (lastDash + dashCoolDown) && currentStamina >= jumpStaminaCost)
                AttemptToDash();
            
        }


        if (!isClimbing)
        {
            if (Input.GetButtonDown("Crouch"))
            {
                isCrouching = true;
                canMove = false;
                this.gameObject.GetComponent<BoxCollider2D>().enabled = false;
            }
            else if (Input.GetButtonUp("Crouch"))
            {
                isCrouching = false;
                canMove = true;
                this.gameObject.GetComponent<BoxCollider2D>().enabled = true;
            }
        }

        if (Input.GetButton("Menu"))
        {
            menu.SetActive(true);
        }

    }

    private void AttemptToDash()
    {
        isDashing = true;
        dashTimeLeft = dashTime;
        lastDash = Time.time;
        staminaLoss.StaminaLoss(jumpStaminaCost * 2);


    }

    public int GetFacingDirection()
    {
        return facingDirection;
    }

    public void CheckDash()
    {

        if (isDashing)
        {
            if (dashTimeLeft > 0)
            {

                
                canMove = false;
                canFlip = false;
                rb.velocity = new Vector2(dashSpeed * facingDirection, 0.0f);
                dashTimeLeft -= Time.deltaTime;
                Physics2D.IgnoreLayerCollision(8, 10);
                
            }

            if (dashTimeLeft <= 0 || isTouchingWall)
            {
                Physics2D.IgnoreLayerCollision(8, 10, false) ;
                isDashing = false;
                canMove = true;
                canFlip = true;
            }

        }
     
     
        
    }

 
    private void CheckHanging()
    {
        hookTimer += Time.deltaTime;

        if (hookTimer > hookLimit)
        {
            canHook= true;
        }
        else
        {
            canHook = false;
        }

        if (canHook && isTouchingHook)
        {
            isHooked = true;
            rb.constraints = RigidbodyConstraints2D.FreezeAll;
        }
        else
        {
            isHooked = false;
        }
       
    }

    private void CheckJump()
    {
        if (jumpTimer > 0 )
        {
            //WallJump
            if (!isGrounded && isTouchingWall )
            {
                WallJump();
            }
            else if (isGrounded )
            {
                NormalJump();
            }

            if (isTouchingLadder)
            {
               
                RopeJump();
            }

            if (canBoost)
            {
                
                BoostJump();
            }
     }

        if (isAttemptingToJump)
        {
            jumpTimer -= Time.deltaTime;
        }

        if (wallJumpTimer > 0)
        {
            if (hasWallJumped && movementInputDirection == -lastWallJumpDirection)
            {
                rb.velocity = new Vector2(rb.velocity.x, 0.0f);
                hasWallJumped = false;
            }
            else if (wallJumpTimer <= 0)
            {
                hasWallJumped = false;
            }
            else
            {
                wallJumpTimer -= Time.deltaTime;
            }
        }
    }


    private void BoostJump()
    {
       
            rb.velocity = new Vector2(rb.velocity.x, boostForce);
            amountOfJumpsLeft--;
            jumpTimer = 0;
            isAttemptingToJump = false;
     
    }

   
    private void NormalJump()
    {
        if (canNormalJump && currentStamina >= jumpStaminaCost)
        {
            
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            amountOfJumpsLeft--;
            jumpTimer = 0;
            isAttemptingToJump = false;
            checkJumpMultiplier = true;
            staminaLoss.StaminaLoss(jumpStaminaCost);

        }
    }

    private void RopeJump()
    {
        if (canRopeJump && currentStamina >= jumpStaminaCost)
        {

           
           
            isWallSliding = false;
            amountOfJumpsLeft = amountOfJumps;
            amountOfJumpsLeft--;
           rb.velocity = new Vector2(jumpForce*facingDirection, jumpForce);
            jumpTimer = 0;
            isAttemptingToJump = false;
            checkJumpMultiplier = true;
            turnTimer = 0;
            canMove = true;
            canFlip = true;
            staminaLoss.StaminaLoss(jumpStaminaCost);

         


        }
    }


    private void WallJump()
    {
        if (canWallJump)
        {
            rb.velocity = new Vector2(rb.velocity.x, 0.0f);
            isWallSliding = false;
            amountOfJumpsLeft = amountOfJumps;
            amountOfJumpsLeft--;
            Vector2 forceToAdd = new Vector2(wallJumpForce * wallJumpDirection.x *-facingDirection, wallJumpForce * wallJumpDirection.y);
            rb.AddForce(forceToAdd, ForceMode2D.Impulse);
            jumpTimer = 0;
            isAttemptingToJump = false;
            checkJumpMultiplier = true;
            turnTimer = 0;
            canMove = true;
            canFlip = true;
            hasWallJumped = true;
            
            
           
            wallJumpTimer = wallJumpTimerSet;
            lastWallJumpDirection = -facingDirection;
           

        }
    }

    private void ApplyMovement()
    {

        if (!isGrounded && !isWallSliding && movementInputDirection == 0 && !knockback )
        {
            rb.velocity = new Vector2(rb.velocity.x * airDragMultiplier, rb.velocity.y);
        }
        else if (canMove && !knockback && !this.anim.GetCurrentAnimatorStateInfo(0).IsName("Attack") && !combat.isBlocking)
        {
            rb.velocity = new Vector2(movementSpeed * movementInputDirection, rb.velocity.y);
        }

        if (isClimbing)
        {

            rb.velocity = new Vector2(rb.velocity.x, verticalMovement * climbingSpeed);
        }


        if (isWallSliding)
        {
            if (rb.velocity.y < -wallSlideSpeed)
            {
                rb.velocity = new Vector2(rb.velocity.x, -wallSlideSpeed);
            }
            if(facingDirection == movementInputDirection)
            {
                rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y);
            }
        }
    }

    public void DisableFlip()
    {
        canFlip = false;
    }

    public void EnableFlip()
    {
        canFlip = true;
    }

    private void Flip()
    {
        if (!isWallSliding && canFlip && !knockback)
        {
            facingDirection *= -1;
            isFacingRight = !isFacingRight;
            transform.Rotate(new Vector3(0, 180, 0));
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(groundCheck.position, groundCheckRadius);
        Gizmos.DrawWireSphere(hookCheck.position, hookCheckRadius);
        Gizmos.DrawWireSphere(boosterCheck.position, boosterCheckRadius);
        Gizmos.DrawWireSphere(backWallCheck.position, wallBackCheckRadius);

        Gizmos.DrawLine(wallCheck.position, new Vector3(wallCheck.position.x + wallCheckDistance, wallCheck.position.y, wallCheck.position.z));
    }

   


}
