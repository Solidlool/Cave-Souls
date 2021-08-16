using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyScript : MonoBehaviour
{
    private GameSystem collectKey;
    private bool pickedUp;
    // Start is called before the first frame update
    void Start()
    {
        collectKey = GameObject.FindGameObjectWithTag("Manager").GetComponent<GameSystem>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player" && !pickedUp)
        {
            pickedUp = true;
            collectKey.KeyCollected();

            Destroy(gameObject);


        }
    }
}
