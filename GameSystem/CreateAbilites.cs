using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class CreateAbilites : MonoBehaviour
{

    public List<GameObject> Abilities = new List<GameObject>();
    public List<GameObject> usedAbilities = new List<GameObject>();
    
    public Transform[] cardLocations;

    public Transform card1;
    public Transform card2;
    public Transform card3;






    // Start is called before the first frame update


    private void Start()
    {

        
    }

  


    // Update is called once per frame
    

    public void CreateAbility()
    {
        Debug.Log("Create abiliy activated again?");
        List<GameObject> abilitiesLeft = new List<GameObject> (Abilities);
        List<GameObject> choices = new List<GameObject>();
        List<Transform> cards = new List<Transform> { card1, card2, card3 };
        while (choices.Count < 3 && abilitiesLeft.Count > 0)
        {
            int index = Random.Range(0, abilitiesLeft.Count);
            choices.Add(abilitiesLeft[index]);
            abilitiesLeft.RemoveAt(index);
        }

        for (int i = 0; i < choices.Count; i++)
        {
            GameObject g = Instantiate(choices[i], cards[i].transform.position, Quaternion.identity) as GameObject;
            g.transform.SetParent(cards[i].transform);
        }

    }
}
