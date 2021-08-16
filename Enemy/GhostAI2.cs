using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostAI2 : MonoBehaviour
{

    public float speed;
    public float stoppingDistance;
    public float retreatDistance;
    public bool facingRight = false;
    public float activateDistance = 40;
   


    private Transform player;

    GameObject playerObject;

    
    

    // Start is called before the first frame update
    void Start()
    {

        playerObject = GameObject.Find("Hero");
        player = playerObject.GetComponent<Transform>();
       
       
            
            }



    // Update is called once per frame
    void Update()
    {
        if (Vector2.Distance(transform.position, player.position) <= activateDistance)
        {


            if (Vector2.Distance(transform.position, player.position) > stoppingDistance)
            {
                transform.position = Vector2.MoveTowards(transform.position, player.position, speed * Time.deltaTime);
            }
            else if (Vector2.Distance(transform.position, player.position) < stoppingDistance && Vector2.Distance(transform.position, player.position) > retreatDistance)
            {
                transform.position = this.transform.position;

            }
            else if (Vector2.Distance(transform.position, player.position) < retreatDistance)
            {
                transform.position = Vector2.MoveTowards(transform.position, player.position, -speed * Time.deltaTime);

            }

            if (player.transform.position.x < gameObject.transform.position.x && facingRight)
            {

                Flip();

            }
            if (player.transform.position.x > gameObject.transform.position.x && !facingRight)
            {

                Flip();
            }
        }
       
    }

 void Flip()
    {
        facingRight = !facingRight;
        transform.Rotate(new Vector3(0, 180, 0));
    }
}
