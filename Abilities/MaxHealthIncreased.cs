using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaxHealthIncreased : MonoBehaviour
{

    int SetHealth = 50;
    GameObject Player;
    // Start is called before the first frame update
 
    public void OnClick()
    {
        Player = GameObject.Find("Hero");

        Player.GetComponent<PlayerStats>().SetHealth(SetHealth);

        Debug.Log("Max health increased by " + SetHealth);
       
    }
}
