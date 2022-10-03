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
        activeAbility.ActivateAbility();
    }

    private void abilityPrimeStart()
    {
        activeAbility.StartAbilityPrime();
    }

    private void abilityPrimeEnd()
    {
        activeAbility.EndAbilityPrime();
    }
}
