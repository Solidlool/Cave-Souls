using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorScript : MonoBehaviour
{
    int keyCount;
    public GameObject OpenDoor;
    private StoryText text;
    
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
        if (collision.gameObject.tag == "Player")
        {
           
            keyCount = GameObject.FindGameObjectWithTag("Manager").GetComponent<GameSystem>().keyCount;
            text = GameObject.FindGameObjectWithTag("Manager").GetComponent<StoryText>();
            if (keyCount >= 3)
            {
                PositiveTake();
            }
            else
            {
                NegativeTake();
            }

            Destroy(gameObject);

        }
    }


    private void PositiveTake()
    {
        OpenDoor.SetActive(true);
        
        text.SecondOutroCall();

        Debug.Log("Open up !");

    }
    private void NegativeTake()
    {
        text.OutroCall();
        
        Debug.Log("I have a bad feeling about this");
    }
}


