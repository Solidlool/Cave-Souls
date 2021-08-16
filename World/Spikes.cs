using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spikes : MonoBehaviour
{
    public int damageToPlayers = 20;
    public PlayerStats Damagedealt;
    public bool staticSpike =false;
    
    

   

   
    private Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {
        
        rb = gameObject.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {

     
       

     
       
    }


   void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            rb.isKinematic = false;
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (!staticSpike)
        {
            if (collision.gameObject.tag == "Player")
            {
                
                Destroy(gameObject);
            }
            else
            {
                Destroy(gameObject);
            }
        }
        
    }
}
