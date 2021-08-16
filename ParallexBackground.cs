using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParallexBackground : MonoBehaviour
{

    private float lengthX, startposX;
    private float lengthY, startposY;
    public GameObject cam;
    public float parallexEffectX;
    public float parallexEffectY;
    void Start()
    {
        startposY = transform.position.y;
        lengthY = GetComponent<SpriteRenderer>().bounds.size.y;
        startposX = transform.position.x;
        lengthX = GetComponent<SpriteRenderer>().bounds.size.x;
    }
    void Update()
    {
        float temp1 = (cam.transform.position.y * (1 - parallexEffectY));
        float dist1 = (cam.transform.position.y * parallexEffectY);

        float temp = (cam.transform.position.x * (1 - parallexEffectX));
        float dist = (cam.transform.position.x * parallexEffectX);
        transform.position = new Vector3(startposX + dist, transform.position.y, transform.position.z);
        if (temp > startposX + lengthX) startposX += lengthX;
        else if (temp < startposX - lengthX) startposX -= lengthX;
    }
}
