using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirePoint : MonoBehaviour
{

    [SerializeField] GameObject bullet;
    public Animator animator;
    public float activateDistance = 40f;
    private Transform player;
    GameObject playerObject;
    
    public float fireRate;
    float nextFire;
    // Start is called before the first frame update

    private void Awake()
    {
        animator = GetComponent<Animator>();
        nextFire = Time.time;
        playerObject = GameObject.Find("Hero");
        player = playerObject.GetComponent<Transform>();
    }


    

    // Update is called once per frame
    void Update()
    {
        CheckIfTimeToFire();
    }

    void CheckIfTimeToFire()
    {
        if (Vector2.Distance(transform.position, player.position) <= activateDistance && Time.time > nextFire)
        {

                Instantiate(bullet, transform.position, Quaternion.identity);
            nextFire = Time.time + fireRate + Random.Range(2, 4);

        }
    }
}
