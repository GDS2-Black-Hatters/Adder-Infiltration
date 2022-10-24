using System;
using UnityEngine;
using static VariableManager;

[CreateAssetMenu(fileName = "NewAbility", menuName = "ScriptableObject/Ability/Ability")]
public class Ability : ScriptableObject
{
    [Serializable]
    public class LevelValues
    {
        [SerializeField] private float minLevelValue;
        [SerializeField] private float maxLevelValue;

        public float GetCurrentValue(float percentage)
        {
            return Mathf.Lerp(minLevelValue, maxLevelValue, percentage);
        }
    }

    [field: SerializeField] public AllAbilities AbilityID { get; protected set; }
    [field: SerializeField] public Sprite Icon { get; protected set; }
    [field: SerializeField] public Upgradeable DefaultUpgrade { get; protected set; } = new(10);
    [field: SerializeField] public LevelValues Cooldown { get; protected set; }
}
