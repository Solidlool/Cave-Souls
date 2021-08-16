using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoubleJump : MonoBehaviour
{

    PlayerController maxJump;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnClick()

    {
        maxJump = GameObject.Find("Hero").GetComponent<PlayerController>();
        maxJump.amountOfJumps = 2;
        Debug.Log("Double Jump Gained");

    }


}
