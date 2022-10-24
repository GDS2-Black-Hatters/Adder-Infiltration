using UnityEngine;

[CreateAssetMenu(fileName = "NewMouse", menuName = "ScriptableObject/Mouse/Mouse")]
public class Mouse : BaseItem
{
    [field: SerializeField] public VariableManager.AllUnlockables Unlockable { get; private set; }
    [field: SerializeField] public override Sprite Icon { get; protected set; }
    [field: SerializeField] public override string Label { get; protected set; }
    [field: SerializeField, Tooltip("Set to 0 for no animation."), Min(0)] public int FPS { get; private set; } = 0;
    [field: SerializeField] public Texture2D[] Frames { get; private set; }
}
