using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingObject : MonoBehaviour
{
     Rigidbody2D rb;

    public float dropTime = 1f;
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
            Invoke("Fall", dropTime);
            
            
        }
    }

    private void Fall()
    {
        rb.isKinematic = false;
        Destroy(gameObject, 2);
    }
 
}
