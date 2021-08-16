using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class UnlockParry : MonoBehaviour
{
    PlayerCombat parryunlock;
    private GameSystem unLock;

    PlayerCombat blockRate;
    private CreateAbilites create;

    public void OnClick()
    {
        
        GameSystem unLock = GameObject.Find("GameManager").GetComponent<GameSystem>();
        unLock.UnlockParry();

        blockRate = GameObject.Find("Hero").GetComponent<PlayerCombat>();
        blockRate.blockingRate = 0.3f;


        GameObject parry = create.Abilities.Where(obj => obj.name == "Parry").SingleOrDefault();
        create.Abilities.Remove(parry);
        create.usedAbilities.Add(parry);

        Debug.Log("Parry Unlocked");
    }

}