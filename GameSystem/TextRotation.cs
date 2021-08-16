using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextRotation : MonoBehaviour
{
    public GameObject canvas;
    private Transform target;
 
   public void Start()
    {
        target = GameObject.Find("TextPlace").transform;
        
    }

   public void Update()
    {
        canvas.transform.position = target.transform.position;
    }
}
