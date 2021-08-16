using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Activated : MonoBehaviour
{

    public bool isActive;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Enabled ()
    {
        Debug.Log("gotit");
        foreach (Transform child in transform)
        {
            child.gameObject.SetActive(true);
        }
    }
}
