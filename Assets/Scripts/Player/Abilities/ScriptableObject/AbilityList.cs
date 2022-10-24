using UnityEngine;

[CreateAssetMenu(fileName = "NewAbilityDictionary", menuName = "ScriptableObject/Ability/Ability Dictionary")]
public class AbilityList : ScriptableObject
{
    [field: SerializeField] public Ability[] Abilities { get; private set; }
}
