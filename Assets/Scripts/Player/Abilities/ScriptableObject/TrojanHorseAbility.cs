using UnityEngine;

[CreateAssetMenu(fileName = "NewTrojanHorse", menuName = "ScriptableObject/Ability/Ability Specific/Trojan Horse")]
public class TrojanHorseAbility : Ability
{
    [field: SerializeField] public LevelValues AbilityDuration { get; private set; }
}
