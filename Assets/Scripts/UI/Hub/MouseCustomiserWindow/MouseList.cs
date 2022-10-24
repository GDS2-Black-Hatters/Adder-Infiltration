using UnityEngine;

[CreateAssetMenu(fileName = "NewMouseList", menuName = "ScriptableObject/Mouse/Mouse List")]
public class MouseList : ScriptableObject
{
    [field: SerializeField] public Mouse[] Mouses { get; private set; } 
}
