using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilityTest : MonoBehaviour
{

    CreateAbilites createAbilites;

    string myName;
    // Start is called before the first frame update
    private void Awake()
    {
        myName = this.gameObject.name;
        createAbilites = GameObject.Find("GameManager").GetComponent<CreateAbilites>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void OnClick()
    {
        createAbilites.Abilities.Remove(this.gameObject);
        Debug.Log("Removed " + myName + " from abilities");
    }
}