using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPoint : MonoBehaviour
{

    
    public GameObject Fire;
    public bool checkPointReached;
    public PlayerStats rl;
    private PlayerStats health;

    
    // Start is called before the first frame update
    void Start()
    {
        rl = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerStats>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
       if (collision.CompareTag ( "Player"))
        {
            Fire.SetActive(true);
            rl.lastCheckPointPos = transform.position;
            health = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerStats>();
            health.HealthRegen(50);
        }
    }
}
