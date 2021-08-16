using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivateEnvironment : MonoBehaviour
{

    public Activated activate;
    public GhostAI2 ghost;
    
    // Start is called before the first frame update
    void Start()
    {
       
        


    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        
        
        if (collision.gameObject.tag == "Enemies")
        {
            
            Activated activate = collision.gameObject.GetComponent<Activated>();
            collision.GetComponent<GhostAI2>().enabled = true;
            activate.Enabled();

            
        }

        else
        {
            return;
        }
    }
}
