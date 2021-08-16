using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{

    public int maxHealth = 100;
    int currentHealth;
    public Animator animator;
    
    public int damageToPlayers = 20;

 
    public Enemy enemy;
    public bool isABat;
    


    Rigidbody2D rb;


    public float knocbackAmount = 100;
    public float knockTime;
    private BatAi batAi;

    Vector2 playerPos;
     
    
    void Start()
    {
 
        enemy= GetComponentInParent<Enemy>();
        currentHealth = maxHealth;
        batAi = this.gameObject.GetComponent<BatAi>();
        

        rb = GetComponent<Rigidbody2D>();
        
        animator = GetComponent<Animator>();
    }

   
    public void TakeDamage(int damage)
    {
        currentHealth -= damage;

      

        if (currentHealth <= 0 )
        {
       
            enemy.GetComponent<Enemy>().Die();
        }
     

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        {
            if (collision.gameObject.CompareTag("Player") && isABat)
            {

                enemy.Die();


            }
        }
    }






}