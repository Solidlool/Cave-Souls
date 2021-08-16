using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillPointManager : MonoBehaviour
{

    LevelingSystem skillPoints;
    SkillPointButton button;
    GameObject skillPanel;

    // Start is called before the first frame update
    void Start()
    {
        button = GameObject.Find("SkillButton").GetComponent<SkillPointButton>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void OnClick()
    {
        skillPoints = GameObject.Find("GameManager").GetComponent<LevelingSystem>();
        skillPoints.skillPoint -= 1;
        
        
        skillPanel = GameObject.Find("SkillPanel");
        
        
        if(skillPoints.skillPoint<= 0)
        {

            skillPanel.SetActive(false);
            button.ButtonActivation();
        }


    }
}
