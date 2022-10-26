using UnityEngine;

[CreateAssetMenu(fileName = "NewBackground", menuName = "ScriptableObject/Desktop/Background")]
public class Background : BaseItem
{
    [field: SerializeField] public VariableManager.AllUnlockables Unlockable { get; private set; }
    [field: SerializeField] public override Sprite Icon { get; protected set; }
    [field: SerializeField] public override string Label { get; protected set; }
}
