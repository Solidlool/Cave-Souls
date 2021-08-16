using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class hotZoneCheck : MonoBehaviour
{
    // Start is called before the first frame update
    private Enemy_behaviour enemyParent;
    private bool inRange;
    private Animator anim;


    private void Awake()
    {
        enemyParent = GetComponentInParent<Enemy_behaviour>();
        anim = GetComponent<Animator>();
    }

    private void Update()
    {
        if(inRange && !anim.GetCurrentAnimatorStateInfo(0).IsName("Skeleton_Attack"))
        {
            enemyParent.Flip();
        }
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            inRange = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            inRange = false;
            gameObject.SetActive(false);
            enemyParent.triggerArea.SetActive(true);
            enemyParent.inRange = false;
            enemyParent.SelectTarget();
        }
    }
}
