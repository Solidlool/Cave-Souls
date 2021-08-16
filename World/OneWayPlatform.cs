using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OneWayPlatform : MonoBehaviour
{

    private PlatformEffector2D effector;
    public float waitTime;
    // Start is called before the first frame update
    void Start()
    {
        effector = GetComponent<PlatformEffector2D>();
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetButtonDown("Crouch") )
        {
            waitTime = 0.5f;
        }
            if (Input.GetButtonDown("Crouch"))

        {
            if (waitTime <= 0)
            {
                effector.rotationalOffset = 180;
                waitTime = 0.5f;
            }
            else
            {
                waitTime -= Time.deltaTime;
            }
        }



        if (Input.GetButton("Jump"))
        {
            effector.rotationalOffset = 0;
        }
    }
}
