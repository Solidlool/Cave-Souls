using UnityEngine;
using UnityEngine.Events;
using System.Collections;

public class CharacterController2D : MonoBehaviour
{
	[SerializeField] private float m_JumpForce = 400f;							// Amount of force added when the player jumps.
	[Range(0, 1)] [SerializeField] private float m_CrouchSpeed = .36f;			// Amount of maxSpeed applied to crouching movement. 1 = 100%
	[Range(0, .3f)] [SerializeField] private float m_MovementSmoothing = .05f;	// How much to smooth out the movement
	[SerializeField] private bool m_AirControl = false;							// Whether or not a player can steer while jumping;
	[SerializeField] private LayerMask m_WhatIsGround;							// A mask determining what is ground to the character
	[SerializeField] private Transform m_GroundCheck;							// A position marking where to check if the player is grounded.
	[SerializeField] private Transform m_CeilingCheck;							// A position marking where to check for ceilings
	[SerializeField] private Collider2D m_CrouchDisableCollider;                // A collider that will be disabled when crouching
	[SerializeField] private LayerMask m_WhatIsWall;
	[SerializeField] private Transform m_GrabPoint;
	
	[SerializeField] private Transform m_PlayerCheck;
	[SerializeField] private Transform m_HookGrabCheck;
	[SerializeField] private LayerMask m_WhatIsChain;
	[SerializeField] private LayerMask m_WhatIsHook;
	public Animator animator;

	

	const float k_HookGrabRadius = .4f;
	const float k_PlayerCheckRadius = .2f;
	const float k_GroundedRadius = 0.4f; // Radius of the overlap circle to determine if grounded
	private bool m_Grounded;            // Whether or not the player is grounded.
	const float k_CeilingRadius = .2f; // Radius of the overlap circle to determine if the player can stand up
	private Rigidbody2D m_Rigidbody2D;
	public bool m_FacingRight = true;  // For determining which way the player is currently facing.
	private Vector3 m_Velocity = Vector3.zero;
	public bool m_WallSliding;

	public bool m_ChainGrab;
	public bool m_HookGrab;
	public bool CanHookGrab;
	
	const float k_GrabRadius = .4f;
	public float slideforce = 1f;

	public float WallJumpspeed;
	
	public Vector2 WallJumpDirection;
	


	public float glideSpeed = 1f;

	private float hookTimer = 0f;
	public float hookLimit = 1f;

	public bool m_notmoving = false;
	private float runTimer = 0f;
	public float runLimit = 1f;
	
	public float rollForce = 400f;



	public PlayerMovement Jump;
	public PlayerMovement Roll;
	public PlayerMovement Glide;
	


	[Header("Events")]
	[Space]

	public UnityEvent OnLandEvent;

	[System.Serializable]
	public class BoolEvent : UnityEvent<bool> { }

	public BoolEvent OnCrouchEvent;
	public BoolEvent OnSlideEvent;
	public BoolEvent OnHookEvent;
	

	private bool m_wasCrouching = false;

	private void Awake()
	{
		m_Rigidbody2D = GetComponent<Rigidbody2D>();

		if (OnLandEvent == null)
			OnLandEvent = new UnityEvent();

		if (OnCrouchEvent == null)
			OnCrouchEvent = new BoolEvent();

		if (OnSlideEvent == null)
			OnSlideEvent = new BoolEvent();

		if (OnHookEvent == null)
			OnHookEvent = new BoolEvent();

		CanHookGrab = true;
		

	}



	private void FixedUpdate()
	{
		bool wasGrounded = m_Grounded;
		m_Grounded = false;


		
		
		hookTimer += Time.deltaTime;
		if (hookTimer > hookLimit)
        {
			CanHookGrab = true;
        }
		// The player is grounded if a circlecast to the groundcheck position hits anything designated as ground
		// This can be done using layers instead but Sample Assets will not overwrite your project settings.
		Collider2D[] colliders = Physics2D.OverlapCircleAll(m_GroundCheck.position, k_GroundedRadius, m_WhatIsGround);
		for (int i = 0; i < colliders.Length; i++)
		{
			if (colliders[i].gameObject != gameObject)
			{
				m_Grounded = true;
				m_WallSliding = false;
				Glide.gliding = false;
				m_AirControl = true;

				if (!wasGrounded && m_Rigidbody2D.velocity.y < 0)
					OnLandEvent.Invoke();
			}
		}

		bool wasWallSliding = m_WallSliding;
		m_WallSliding = false;

		Collider2D[] wallcolliders = Physics2D.OverlapCircleAll(m_GrabPoint.position, k_GrabRadius, m_WhatIsWall);
		for( int i=0; i< wallcolliders.Length; i++)
        {
			if (m_Grounded == false)
			{

				if (wallcolliders[i].gameObject != gameObject)
				{
					m_WallSliding = true;
					OnSlideEvent.Invoke(true);

				}
			}
			
        }


		Collider2D[] chaincolliders = Physics2D.OverlapCircleAll(m_PlayerCheck.position, k_PlayerCheckRadius, m_WhatIsChain);
		for (int i = 0; i < chaincolliders.Length; i++)
		{
			if (m_Grounded == false)
			{

				if (chaincolliders[i].gameObject != gameObject)
				{
					m_ChainGrab = true;
					m_AirControl = true;

				}
			}

		}

		Collider2D[] hookcolliders = Physics2D.OverlapCircleAll(m_HookGrabCheck.position, k_HookGrabRadius, m_WhatIsHook);
		for (int i = 0; i < hookcolliders.Length; i++)
		{
			

				if (hookcolliders[i].gameObject != gameObject)
				{
				if (CanHookGrab )
				{
					m_HookGrab = true;

					
					m_Rigidbody2D.constraints = RigidbodyConstraints2D.FreezeAll;
					
					hookTimer += Time.deltaTime;

					OnHookEvent.Invoke(true);


				}
				}
			
		}
	

		
       

		if (m_WallSliding)
        {
			/*m_Rigidbody2D.velocity = new Vector2(m_Rigidbody2D.velocity.x, - slideforce);*/
			m_Rigidbody2D.velocity = new Vector2(m_Rigidbody2D.velocity.x, Mathf.Clamp(m_Rigidbody2D.velocity.y,-slideforce, float.MaxValue));

			m_Grounded = false;
			Glide.gliding = false;
			m_AirControl = false;


		}
	





		/*if (Glide.gliding&& ! m_WallSliding)
		{
			m_AirControl = true;
         if  (!m_FacingRight)
			{
				m_Rigidbody2D.velocity = new Vector2(m_Rigidbody2D.velocity.x, Mathf.Clamp(m_Rigidbody2D.velocity.y, -glideSpeed, float.MaxValue));
				Debug.Log("Repulok");


			}
			else
			{
				m_Rigidbody2D.velocity = new Vector2(m_Rigidbody2D.velocity.x, Mathf.Clamp(m_Rigidbody2D.velocity.y, -glideSpeed, float.MaxValue));
				Debug.Log("Repulok");

			}
		
		}*/
		if (Roll.roll && runTimer > +runLimit && m_Grounded )
		{
			if (!m_FacingRight)
			{
				m_Rigidbody2D.velocity = new Vector2(-rollForce, m_Rigidbody2D.velocity.y);

				Debug.Log("keep rollin");
				runTimer = 0f;
				animator.SetTrigger("Rolling");
				Roll.roll = false;
				
				
					
			}
            if (m_FacingRight)
			{
				m_Rigidbody2D.velocity = new Vector2(rollForce, m_Rigidbody2D.velocity.y);


				Debug.Log("keep rollin");
				runTimer = 0f;
				animator.SetTrigger("Rolling");
				Roll.roll = false;
				
			

			}

		
		}
		if (Mathf.Approximately(m_Rigidbody2D.velocity.y, m_Rigidbody2D.velocity.x) && m_Grounded)
		{

			runTimer = 0f;
			Roll.roll = false;
		}
		else
		{

			runTimer += Time.deltaTime;

		}


	}


	public void Move(float move, bool crouch, bool jump, bool gliding,bool roll)
	{
		// If crouching, check to see if the character can stand up
		if (!crouch)
		{
			// If the character has a ceiling preventing them from standing up, keep them crouching
			if (Physics2D.OverlapCircle(m_CeilingCheck.position, k_CeilingRadius, m_WhatIsGround))
			{
				crouch = true;
			}
		}

        

		//only control the player if grounded or airControl is turned on
		if (m_Grounded || Glide.gliding || m_AirControl)
		{
            if (roll)
            {
				move *= m_CrouchSpeed;
            }
          
			// If crouching
			if (crouch )
			{
				if (!m_wasCrouching)
				{
					m_wasCrouching = true;
					OnCrouchEvent.Invoke(true);
				}



				// Reduce the speed by the crouchSpeed multiplier
				move *= m_CrouchSpeed;
               

				// Disable one of the colliders when crouching
				if (m_CrouchDisableCollider != null)
					m_CrouchDisableCollider.enabled = false;
			} else
			{
				// Enable the collider when not crouching
				if (m_CrouchDisableCollider != null)
					m_CrouchDisableCollider.enabled = true;

				if (m_wasCrouching)
				{
					m_wasCrouching = false;
					OnCrouchEvent.Invoke(false);
				}

				
			}

			// Move the character by finding the target velocity
			Vector3 targetVelocity = new Vector2(move * 10f, m_Rigidbody2D.velocity.y);
			// And then smoothing it out and applying it to the character

			m_Rigidbody2D.velocity = Vector3.SmoothDamp(m_Rigidbody2D.velocity, targetVelocity, ref m_Velocity, m_MovementSmoothing);


			// If the input is moving the player right and the player is facing left...
			if (move > 0 && !m_FacingRight)
			{
				
				// ... flip the player.
				Flip();
			}
			// Otherwise if the input is moving the player left and the player is facing right...
			else if (move < 0 && m_FacingRight)
			{
				
				// ... flip the player.
				Flip();
			}

           
		}


		// If the player should jump...
		if (m_Grounded && jump || m_ChainGrab&& jump )
		{
			// Add a vertical force to the player.
			
			
				
				
				runTimer = 0f;

				m_Rigidbody2D.velocity = new Vector2(m_Rigidbody2D.velocity.x, m_JumpForce);
				m_Grounded = false;
				m_ChainGrab = false;
				
			
		}
	

		if (m_WallSliding && jump)
		{

			Debug.Log("imjumping");
			{

				
				if (!m_FacingRight)
			    {

					/*m_Rigidbody2D.velocity = new Vector2(m_WalljumpX, m_JumpForce); */

					
					

					m_Rigidbody2D.velocity = new Vector2(m_Rigidbody2D.velocity.x, 0.0f);
					m_WallSliding = false;
					Vector2 forceToAdd = new Vector2(WallJumpspeed * WallJumpDirection.x, WallJumpspeed * WallJumpDirection.y);
					m_Rigidbody2D.AddForce(forceToAdd, ForceMode2D.Impulse);


					m_Grounded = false;
				    
				    /*m_AirControl = true;*/
					
					Flip();

				}

			    else
			    {
					

					m_Rigidbody2D.velocity = new Vector2(m_Rigidbody2D.velocity.x, 0.0f);
					m_WallSliding = false;
					Vector2 forceToAdd = new Vector2(WallJumpspeed * -WallJumpDirection.x ,WallJumpspeed * WallJumpDirection.y);
					m_Rigidbody2D.AddForce(forceToAdd, ForceMode2D.Impulse);
					/*m_Rigidbody2D.velocity = new Vector2(-m_WalljumpX, m_JumpForce); */

					m_Grounded = false;
				
				/*m_AirControl = true;*/
				Debug.Log("im walljumping");
					Flip();
		

				}

		    }
			

		}

		if (m_HookGrab && jump)
		{


			m_HookGrab = false;
			Debug.Log("imhookjumping");
			CanHookGrab = false;
			m_Rigidbody2D.constraints = RigidbodyConstraints2D.None;
			m_Rigidbody2D.constraints = RigidbodyConstraints2D.FreezeRotation;

			m_Rigidbody2D.velocity = new Vector2(m_Rigidbody2D.velocity.x, m_JumpForce);


			hookTimer = 0f;

		}
	}


	private void Flip()
	{
		// Switch the way the player is labelled as facing.
		m_FacingRight = !m_FacingRight;

		// Multiply the player's x local scale by -1.
		/*Vector3 theScale = transform.localScale;
		theScale.x *= -1;
		transform.localScale = theScale;*/
		transform.Rotate(new Vector3(0, 180, 0));
	}
}
