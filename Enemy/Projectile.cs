using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    
    public float speed;

    Rigidbody2D rb;
    public int damageToPlayers = 20;

    private Transform target;

    private Vector2 moveDirection;

    public bool parriedProjectile = false;
    public bool homingProjectile = true;
    


    


    // Start is called before the first frame update
    void Start()
    {
           
       
            rb = GetComponent<Rigidbody2D>();
            target = GameObject.FindGameObjectWithTag("Target").transform;
            moveDirection = (target.transform.position - transform.position).normalized * speed;

            if (homingProjectile)
            {
                rb.velocity = new Vector2(moveDirection.x, moveDirection.y);
            }

        if(target.transform.position.x> transform.position.x )
        {
            rb.velocity = new Vector2(1*speed, rb.velocity.y);
        }
        if (target.transform.position.x < transform.position.x)
        {
            rb.velocity = new Vector2(1 * -speed, rb.velocity.y);
        }





            Destroy(gameObject, 5f);
    }



    void OnCollisionEnter2D(Collision2D collision)
    {
      
       

        if (collision.gameObject.CompareTag("Projectile"))
        {
            Destroy(collision.gameObject);
        }

        if (collision.gameObject.CompareTag("Enemies")&& parriedProjectile)
        {
            collision.gameObject.GetComponent<Enemy>().Die();
            Destroy(gameObject);
            
           
        }

        if (collision.gameObject.CompareTag("Platform"))
        {
            Destroy(gameObject);
        }

        if (collision.gameObject.CompareTag("Player"))
        {
            Destroy(gameObject);
        }


    }

  





}
    
