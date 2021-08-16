using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundTrigger : MonoBehaviour
{

    public GameObject background2;
    public GameObject background1;
    public GameObject farBackGround;
    public bool secondSwitch;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player" && !secondSwitch)
        {
            background1.SetActive(false);
            farBackGround.SetActive(true);


           

        }
        if (collision.gameObject.tag == "Player" && secondSwitch) 
        {
            background2.SetActive(true);
        }

    }

   
}
