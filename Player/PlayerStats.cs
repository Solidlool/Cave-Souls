using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerStats : MonoBehaviour
{
    public int maxHealth = 100;
    public int currentHealth;

    private GameSystem sys;
    public HealthBar healthbar;
    public StaminaBar staminabar;
    public PlayerMovement playerMovement;

    public float maxStamina = 100.0f;
    public float currentStamina;
    
    public float StaminaRegen = 1f;
    public float notMovingRegen;
    public float healthPerSecond = 2f;
    public Vector3 lastCheckPointPos;

    public Animator animator;
    Rigidbody2D _rb;
    EnemyHealth enemy;

    public float knocbackAmount = 100;
    public float knockTime;
    Spikes spikes;
    public bool damagedealt = false;

    public float damageImun = 1f;
    public float damageTimer = 1f;
 
    public PlayerCombat isBlocking;
    
    public bool isDashing;
    public bool facingRight;
    public bool blocking;
  
    Projectile projectile;

   

   



    // Start is called before the first frame update
    void Start()
    {
        sys = GameObject.FindGameObjectWithTag("Manager").GetComponent<GameSystem>();
        
        maxHealth = GameObject.FindGameObjectWithTag("Manager").GetComponent<GameSystem>().maxHp;
        transform.position = lastCheckPointPos;
        currentHealth = maxHealth;
        currentStamina = maxStamina;
        _rb = GetComponent<Rigidbody2D>();

        healthbar.SetMaxHealth(maxHealth);
        staminabar.SetMaxStamina(maxStamina);

        
       
        Blocking canBeBlocked = GameObject.Find("Hero").GetComponent<Blocking>();

        PlayerCombat isBlocking = this.gameObject.GetComponent<PlayerCombat>();
        CharacterController2D facingRight = this.gameObject.GetComponent<CharacterController2D>();
    }


    public void StaminaLoss(float staminaCost)
    {
        currentStamina -= staminaCost;
        if(currentStamina <= staminaCost)
        {
            staminabar.SetStamina(currentStamina);   
            Debug.Log("I am out of stamina");
        }
    }

    public void DamageTaken(int damageToPlayers)
    {

        if (damageTimer > damageImun)
        {
            if (damageToPlayers >= 1)
            {
                currentHealth -= damageToPlayers;
                healthbar.SetHealth(currentHealth);

                if (currentHealth <= 0)
                {
                    Die();
                    
    
                }
                else
                {
                    animator.SetTrigger("Damaged");
                    damageTimer = 0;

                }
            }
        }
 
    }


    private void Update()
    {
  
        damageTimer += Time.deltaTime;

        if( _rb.velocity.sqrMagnitude <= 0.1f  && currentStamina < maxStamina)
        {
            currentStamina += StaminaRegen * Time.deltaTime * notMovingRegen;
            staminabar.SetStamina(currentStamina);

        }

        else if (currentStamina < maxStamina && _rb.velocity.sqrMagnitude >= 0.1f)
        {
            currentStamina += StaminaRegen * Time.deltaTime;
            staminabar.SetStamina(currentStamina);
        }

    }



    public void Die()
    {

        Debug.Log("Player Died");

        animator.SetTrigger("Died");
        damageImun = 100f;
        this.gameObject.GetComponent<PlayerCombat>().enabled = false;
        this.gameObject.GetComponent<PlayerController>().enabled = false;

        _rb.constraints = RigidbodyConstraints2D.FreezeAll;
        GetComponent<Collider2D>().enabled = false;
        Invoke("Respawn",  2);
        
        
        

    }

    void Respawn()
    {
        currentHealth = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerStats>().maxHealth;
        sys.ReLoadScene();
        transform.position = lastCheckPointPos;
    }

    public void FixedUpdate()
    {
        blocking  = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerCombat>().isBlocking;
        facingRight = this.gameObject.GetComponent<PlayerController>().isFacingRight;
        

    }
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Projectile" )
        {
            projectile = collision.gameObject.GetComponent<Projectile>();
            DamageTaken(projectile.damageToPlayers);
            
            Destroy(collision.gameObject);

        }

            if (collision.gameObject.tag == "Enemies" )
        {

                Vector2 direction = (transform.position - collision.transform.position).normalized;
                //_rb.AddForce(direction * knocbackAmount);

                Debug.Log(knocbackAmount);

                enemy = collision.gameObject.GetComponent<EnemyHealth>();
                

                DamageTaken(enemy.damageToPlayers);

        }
        

        if (collision.gameObject.tag == "Hazard")
        {
            spikes = collision.gameObject.GetComponent<Spikes>();
            DamageTaken(spikes.damageToPlayers);
             
        }

    }
    public void DamageTakenRight(int damageToPlayers)
    {
        if( !facingRight && blocking || facingRight && !blocking || !facingRight && !blocking)
        {
            DamageTaken(damageToPlayers);
           

        }
        if(facingRight && blocking)
        {
            Debug.Log("blocked");
        }
    }

    public void DamageTakenLeft(int damageToPlayers)
    {
        if (facingRight && blocking || !facingRight && !blocking || facingRight && !blocking)
        {
            DamageTaken(damageToPlayers);
           

        }
        if (!facingRight && blocking)
        {
            
            
        }
    }

    
    public void SetHealth(int health)
    {
        maxHealth += health;
        currentHealth += health;
        healthbar.SetMaxHealth(maxHealth);
    }

    public void SetStamina(int stamina)
    {
        maxStamina += stamina;
        currentStamina += stamina;
        staminabar.SetMaxStamina(maxStamina);
    }

    public void HealthRegen(int health)
    {
        currentHealth += health;
        if (currentHealth > maxHealth)
        {
            currentHealth = maxHealth;
            healthbar.SetHealth(currentHealth);
        }

    }
    



}
