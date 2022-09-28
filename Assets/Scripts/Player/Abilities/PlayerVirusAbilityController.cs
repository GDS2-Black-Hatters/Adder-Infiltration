using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerVirusAbilityController : MonoBehaviour
{
    [SerializeField] private AbilityBase activeAbility;

    private void Start()
    {
        GameManager.LevelManager.player.virusController.onAbilityTriggerStart += abilityTriggerStart;
    }

    private void abilityTriggerStart()
    {
        activeAbility.ActivateAbility();
    }
}
