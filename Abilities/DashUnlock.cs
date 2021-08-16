using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class DashUnlock : MonoBehaviour
{

    private GameSystem unLock;

    private CreateAbilites create;
    // Start is called before the first frame update


    public void OnClick()
    {

        GameSystem unLock = GameObject.Find("GameManager").GetComponent<GameSystem>();
        CreateAbilites create = GameObject.Find("GameManager").GetComponent<CreateAbilites>();

        unLock.UnlockDash();

            
       
        Debug.Log(" dash unlocked");

        GameObject dash = create.Abilities.Where(obj => obj.name == "Dash").SingleOrDefault();
        create.Abilities.Remove(dash);
        create.usedAbilities.Add(dash);

    }
}
