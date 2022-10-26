using UnityEngine;

[CreateAssetMenu(fileName = "NewAbilityList", menuName = "ScriptableObject/Ability/Ability List")]
public class AbilityList : ScriptableObject
{
    [field: SerializeField] public Ability[] Abilities { get; private set; }
}
