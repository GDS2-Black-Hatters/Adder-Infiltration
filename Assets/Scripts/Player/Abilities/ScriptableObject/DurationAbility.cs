using UnityEngine;

[CreateAssetMenu(fileName = "NewDurationAbility", menuName = "ScriptableObject/Ability/Ability Specific/With Duration")]
public class DurationAbility : Ability
{
    [field: SerializeField] public LevelValues AbilityDuration { get; private set; }
}
