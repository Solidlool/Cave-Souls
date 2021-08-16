using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelingSystem : MonoBehaviour
{
    public int skillPoint;
    public int level;
    public int experiencePoints;
    public int experienceToNextLevel;
    CreateAbilites gainability;
    SkillPointButton button;
    
    

    public Slider levelUpBar;
   /* public Text CurrentLevel;*/


    public void Start()
    {
        skillPoint = 0;
        level = 1;
        experiencePoints = 0;
        experienceToNextLevel = 100;

        levelUpBar.value = experiencePoints;
        levelUpBar.maxValue = experienceToNextLevel;
        
        button = GameObject.Find("SkillButton").GetComponent<SkillPointButton>();
        

        /*CurrentLevel.text = "Level : 1";*/
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.T))
        {
            AddExperience(51);
        }


    }



    public void AddExperience(int experienceAmount)
    {
        Debug.Log(experienceAmount + " experience gained");
        experiencePoints += experienceAmount;

        levelUpBar.value = experiencePoints;

        if(experiencePoints>= experienceToNextLevel)
        {
            LevelUp();
        }
    }

    void LevelUp()
    {
        skillPoint += 1;
        button.ButtonActivation();
        gainability = GameObject.Find("GameManager").GetComponent<CreateAbilites>();
        gainability.CreateAbility();
        
       
        experiencePoints = 0;
        levelUpBar.value = experiencePoints;

        experienceToNextLevel += 10;
        levelUpBar.maxValue = experienceToNextLevel;


        level +=1;
        Debug.Log("You are level " + level);
        /* CurrentLevel.text= "Level : " + level.ToString();*/
    }
}
