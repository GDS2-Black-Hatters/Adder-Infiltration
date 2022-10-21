using UnityEngine;

[CreateAssetMenu(fileName = "NewEMPAbility", menuName = "ScriptableObject/Ability/Ability Specific/EMP")]
public class EMPAbility : Ability
{
    [field: SerializeField] public LevelValues EffectRange { get; private set; }
    [field: SerializeField] public LevelValues ChargeUpTime { get; private set; }
    public float EffectDuration { get; private set; } = 3f;
    [field: SerializeField] public LevelValues StunDuration { get; private set; }
}