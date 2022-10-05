using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerVirusAbilityController : MonoBehaviour
{
    [SerializeField] private AbilityBase activeAbility;

    private void Start()
    {
        GameManager.LevelManager.player.virusController.onAbilityTrigger += abilityTriggerStart;
        GameManager.LevelManager.player.virusController.onAbilityPrimeStart += abilityPrimeStart;
        GameManager.LevelManager.player.virusController.onAbilityPrimeEnd += abilityPrimeEnd;
    }

    private void abilityTriggerStart()
    {
        if(activeAbility == null) return;
        activeAbility.ActivateAbility();
    }

    private void abilityPrimeStart()
    {
        if(activeAbility == null) return;
        activeAbility.StartAbilityPrime();
    }

    private void abilityPrimeEnd()
    {
        if(activeAbility == null) return;
        activeAbility.EndAbilityPrime();
    }

    public void changeAbility(AbilityBase newAbility)
    {
        if(activeAbility != null)
        {
            activeAbility.EndAbilityPrime();
            Destroy(activeAbility.gameObject);
        }

        activeAbility = newAbility;
        activeAbility.transform.SetParent(transform);
    }
}
