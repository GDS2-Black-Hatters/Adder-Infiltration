using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "newThemeSet", menuName = "ScriptableObject/Theme/ThemeSet")]
public class ThemeSetSO : ScriptableObject
{
    [field: SerializeField] public Texture globalColorPalletTexture{ get; private set; }
}
