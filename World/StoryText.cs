using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StoryText : MonoBehaviour
{

    private string[] intro;
    private string[] secondIntro;
    private string[] outro;
    private string[] secondOutro;
    private string[] onPickUp;

    public Text speechText;
    // Start is called before the first frame update
    void Start()
    {
        intro = new string[]{ "I have seen this before", "Not this again", "Lets try this again", "I have failed",};
        secondIntro = new string[] { "Something is different..", "What is the key to all this ?", "What did I miss?", "I am back.. again??"};
        outro = new string[] { "Will this nightmare ever end?", "I failed.. again", "I must have missed something..", "Where could it be?!" };
        secondOutro = new string[] { "Is it finally over?", " So this is it" };
        onPickUp = new string[] { "What was that?", "Is this helps me to leave?", "If there is a key, there must a door somewhere", "Heavier than it looks", "Now where can i find the others?", };
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.T))
        {
            IntroCall();
        }
    }

    public void IntroCall()
    {
        speechText.text = intro[Random.Range(0, intro.Length)];
        speechText.gameObject.SetActive(true);
        Debug.Log(speechText);
        Invoke("TurnOffText",5);   
    }

    public void OutroCall()
    {
        speechText.text = outro[Random.Range(0, intro.Length)];
        speechText.gameObject.SetActive(true);
        Debug.Log(speechText);
        Invoke("TurnOffText", 5);
    }
    public void SecondOutroCall()
    {
        speechText.text = secondOutro[Random.Range(0, intro.Length)];
        speechText.gameObject.SetActive(true);
        Debug.Log(speechText);
        Invoke("TurnOffText", 5);
    }
    public void SecondIntroCall()
    {
        speechText.text = secondIntro[Random.Range(0, intro.Length)];
        speechText.gameObject.SetActive(true);
        Debug.Log(speechText);
        Invoke("TurnOffText", 5);
    }

    public void KeyPickUpText()
    {
        speechText.text = onPickUp[Random.Range(0, intro.Length)];
        speechText.gameObject.SetActive(true);
        Debug.Log(speechText);
        Invoke("TurnOffText", 5);
    }

    private void TurnOffText()
    {
        speechText.gameObject.SetActive(false);
    }
}
