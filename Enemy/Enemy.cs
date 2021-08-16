using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{

    
    public Animator animator;
    Rigidbody2D rb;
    public GameObject head, leftArm, rightArm, leftLeg, rightLeg, torso;

    public float knocbackAmount = 100;
    public float knockTime;
    public bool canExplode;
    public int Experience = 10;
    

    LevelingSystem gainXp;

    

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();    
    }


    public void TakeDamage(int damage)
    {

        animator.SetTrigger("Hurt");
    }



    public void Die() 
    
    
    {
        gainXp = GameObject.Find("GameManager").GetComponent<LevelingSystem>();
        gainXp.GetComponent<LevelingSystem>().AddExperience(Experience);


        rb.isKinematic = true;
        rb.constraints = RigidbodyConstraints2D.FreezeAll;
        if (canExplode)
        {
            Destroy(transform.parent.gameObject);
            Debug.Log("enemy died");
            Instantiate(head, transform.position, Quaternion.identity);
            Instantiate(leftArm, transform.position, Quaternion.identity);
            Instantiate(rightArm, transform.position, Quaternion.identity);
            Instantiate(leftLeg, transform.position, Quaternion.identity);
            Instantiate(rightLeg, transform.position, Quaternion.identity);
            Instantiate(torso, transform.position, Quaternion.identity);

            


            foreach (Transform child in transform)
            {
                child.gameObject.SetActive(false);
            }


            rb.constraints = RigidbodyConstraints2D.FreezeAll;
           
            Destroy(transform.parent.gameObject);
            this.enabled = false;

            
           
        }


        else
        {
            Debug.Log("enemy died");

            animator.SetBool("isDead", true);

            foreach (Transform child in transform)
            {
                child.gameObject.SetActive(false);
            }
           
            GetComponent<Collider2D>().enabled = false;
            Destroy(rb);
   
            Destroy(gameObject, 1f);
        }

    }


    public void Update()
    {
        
    }


}
    // Update is called once per frame
    
