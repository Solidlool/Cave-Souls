using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombat : MonoBehaviour
{


    public Animator animator;
    public Transform attackPoint;
    public Transform airAttackPoint;
    public float attackRange = 0.5f;
    public float airAttackRange = 0.5f;
    public LayerMask enemyLayers;
    public int attackDamage = 40;
    public float attackRate = 2f;
    float nextAttackTime = 0f;

    private PlayerStats PlayerStats;
    private PlayerStats staminacost;
    
    
    public float damageTime;
    public float currentStamina;
    public float AttackStaminaCost = 60f;

    public float canAttack = 1f;

    public bool canBlock;
    public bool parryUnlocked;
  

    Rigidbody2D rb;


    float nextParryTime = 0f;
    public float parryRate = 2f;
    public float parryTime = 1f;
    float blockingTime;
  
    public float blockingRate;
   
    public bool isBlocking;
    public bool isParrying;
    private PlayerController isgrounded;


    private void Start()
    {
       
        PlayerStats = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerStats>();
        staminacost = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerStats>();
        isgrounded = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        rb = gameObject.GetComponent<Rigidbody2D>();

    }


    void Update()
    {
        damageTime = PlayerStats.damageTimer;
        currentStamina = PlayerStats.currentStamina;
        nextParryTime += Time.deltaTime;
        parryUnlocked = GameObject.FindGameObjectWithTag("Manager").GetComponent<GameSystem>().parryUnlocked;


        if (Time.time >= nextAttackTime && damageTime >= canAttack && currentStamina >= AttackStaminaCost&& isgrounded.isGrounded)
        {
            if (Input.GetButton("Fire1"))
            { 
                animator.SetTrigger("Attack");       
                nextAttackTime = Time.time + 1f / attackRate;
            }
         
        }

        if (Time.time >= nextAttackTime && damageTime >= canAttack && currentStamina >= AttackStaminaCost && !isgrounded.isGrounded)
        {
            if (Input.GetButton("Fire1"))
            {
                animator.SetTrigger("JumpAttack");
                nextAttackTime = Time.time + 1f / attackRate;
            }

        }


        //if the block button hold down less than the parry timer than the player parryies, parryRate control how often the player can parry a second
            if (Input.GetButton("Fire2") )
        {

            blockingTime += Time.deltaTime;

            if (blockingTime <= parryTime && nextParryTime >= parryRate && parryUnlocked)
            {
                isParrying = true;

            }
            else
            {
                isParrying = false;
            }
            if (blockingTime >= blockingRate && isgrounded.isGrounded)
            {
                animator.SetBool("isBlocking", true);

                isBlocking = true;
                rb.constraints = RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezeRotation;
            }
        }
        if (Input.GetButtonUp("Fire2"))
        {
            canBlock = false;
            blockingTime = 0f;
            isParrying = false;
            isBlocking= false;
            nextParryTime = 0f;
            animator.SetBool("isBlocking", false);
            rb.constraints = RigidbodyConstraints2D.FreezeRotation;
        }

    }
    public void ParryFinished()
    {
        canBlock = true;
        blockingTime = 0f;
    }


    public void Attack()
    {

        
        staminacost.StaminaLoss(AttackStaminaCost);
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position,attackRange,enemyLayers);

        foreach(Collider2D enemy in hitEnemies)
        {
            Debug.Log("IHIT");
            enemy.GetComponent<EnemyHealth>().TakeDamage(attackDamage);
        }
    }

    public void JumpAttack()
    {
        staminacost.StaminaLoss(AttackStaminaCost);
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(airAttackPoint.position, airAttackRange, enemyLayers);

        foreach(Collider2D enemy in hitEnemies)
        {
            
            enemy.GetComponent<EnemyHealth>().TakeDamage(attackDamage);
}
    }

    private void OnDrawGizmosSelected()
    {
        if (attackPoint == null)
            return;

        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
        Gizmos.DrawWireSphere(airAttackPoint.position, airAttackRange);
    }


    public void UnFreezeMovement()
    {
      
        {
            rb.constraints = RigidbodyConstraints2D.FreezeRotation;
            
        }
    }
    
}
