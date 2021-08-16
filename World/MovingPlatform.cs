using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{

    public Transform target1;

    public Transform target2;

    public float speed = 2.0f;

    Vector3 currentTarget;

    

    


    // Start is called before the first frame update
    void Start()
    {
        currentTarget = target1.position;
    }

    // Update is called once per frame
    void Update()
    {
       
    }

    private void FixedUpdate()
    {
        var step = speed * Time.deltaTime;

        if(transform.position == target1.position)
        {
            currentTarget = target2.position;
        }
        else if (transform.position == target2.position)
        {
            currentTarget = target1.position;
        }

        transform.position = Vector3.MoveTowards(transform.position, currentTarget, step);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player" || collision.gameObject.tag == "Enemies")
        {
            collision.collider.transform.SetParent(transform);
        }
    }



    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            collision.collider.transform.SetParent(null);
        }
    }
}
