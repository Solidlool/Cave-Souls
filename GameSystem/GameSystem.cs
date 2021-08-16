using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameSystem : MonoBehaviour
{

    private PlayerStats stats;
    private PlayerCombat combat;
    public int maxHp;
    public float maxSt;
    public bool dashUnlocked;
    public bool parryUnlocked;
    public bool doubleJumpUnlocked;
    public Text keyCounter;
    private StoryText text;


    public int keyCount;
   
    

  
    // Start is called before the first frame update
    void Start()
    {
        combat = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerCombat>();
        maxHp = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerStats>().maxHealth;
        keyCount = 0;
        text = GameObject.FindGameObjectWithTag("Manager").GetComponent<StoryText>();
        stats = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerStats>();
        
        keyCounter.text = keyCount + "  out of 3 keys collected"; 


    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void QuitApp()
    {
        Application.Quit();
    }




    public void Respawn()
    {
        maxHp = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerStats>().maxHealth;
        Invoke("ReLoadScene", 2);
    }

    public void ReLoadScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void UnlockDash()
    {
        dashUnlocked = true;  
    }

    public void UnlockParry()
    {
        parryUnlocked = true;
    }

    public void SetMaxHp()
    {

    }

    public void Restart()
    {
        SceneManager.LoadScene("GameScene");
    }

    public void KeyCollected()
    {

        keyCount++;   
        keyCounter.text = keyCount + "  out of 3 keys collected";
        keyCounter.gameObject.SetActive(true);
        text.KeyPickUpText();
        Invoke("TurnOffText", 5);


    }

   private void TurnOffText()
    {
        keyCounter.gameObject.SetActive(false);
    }




   
}
