using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HazardDamage : MonoBehaviour
{

    public int damageToPlayers = 20;
    public PlayerStats playerStat;


    
    // Start is called before the first frame update
    void Awake()
    {
        playerStat = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerStats>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            playerStat.DamageTaken(damageToPlayers);
            Debug.Log("hazardDamage");
        }
    }


}
