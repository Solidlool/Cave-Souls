using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillPointButton : MonoBehaviour
{

    int skillPoint;
    public GameObject manager;
    public Toggle toggle;
    public Image image;
    
   
    // Start is called before the first frame update
    void Start()
    {
        

        
        
    }

    // Update is called once per frame
    void Update()
    {

     
    }


    public void ButtonActivation()
    {
        Debug.Log("ButtonActive");

        skillPoint = manager.GetComponent<LevelingSystem>().skillPoint;
        if (skillPoint >= 1)
        {

            toggle.interactable = true;
            this.gameObject.GetComponent<Image>().enabled = true;
        }
        else
        {
            toggle.interactable = false;
            this.gameObject.GetComponent<Image>().enabled = false;
        }
    }
}
