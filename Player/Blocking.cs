using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blocking : MonoBehaviour
{
    public PlayerCombat player_combat;

    public bool successfullParry;

    Vector3 bulletvelocity;
    public Animator animator;
    Vector3 direction;

    public PlayerController facingright;

    


    // Start is called before the first frame update
    void Start()
    {
        PlayerCombat player_combat = GameObject.Find("Hero").GetComponent<PlayerCombat>();
        GetComponent<Collider2D>().enabled = false;
        PlayerController facingright = GameObject.Find("Hero").GetComponent<PlayerController>();

    }

    // Update is called once per frame
    void Update()
    {
        if (player_combat.isBlocking || player_combat.isParrying)
        {
            
            GetComponent<Collider2D>().enabled = true;

        }
        if (!player_combat.isBlocking && !player_combat.isParrying)
        {
            GetComponent<Collider2D>().enabled = false;
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Projectile") || collision.gameObject.CompareTag("EnemyWeapon"))
        {
           


            if (collision.gameObject.CompareTag("Projectile"))
            {

                Rigidbody2D enemyRigidbody = collision.gameObject.GetComponent<Rigidbody2D>();
                Projectile parriedP = collision.gameObject.GetComponent<Projectile>();

                if (player_combat.isParrying)
                {

                    if (facingright.isFacingRight)
                    {
                        direction = Vector3.right;
                    }
                    else
                    {
                        direction = Vector3.left;
                    }

                    Debug.Log("parried");
                    animator.SetTrigger("Parry");
                    successfullParry = true;
                    bulletvelocity = enemyRigidbody.velocity;
                    Vector3 reflectedVelocity = Vector3.Reflect(bulletvelocity * 2, direction);

                    enemyRigidbody.velocity = reflectedVelocity;
                    parriedP.parriedProjectile = true;
                   

                }

                if (!player_combat.isParrying)
                {
                    Debug.Log("blocked");
                    Destroy(collision.gameObject);
                }

            }
        }
    }
   
}
