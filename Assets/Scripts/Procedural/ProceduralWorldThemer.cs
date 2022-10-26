using UnityEngine;

public class ProceduralWorldThemer : MonoBehaviour
{
    [SerializeField] private ThemeSetSO[] themes;

    private void Start()
    {
        ChangeTheme();
    }

#if UNITY_EDITOR
    [SerializeField] private bool changeTheme;
    private void Update()
    {
        if (changeTheme)
        {
            ChangeTheme();
            changeTheme = false;
        }
    }
#endif

    private void ChangeTheme()
    {
        ThemeSetSO theme = themes[Random.Range(0, themes.Length)];
        Shader.SetGlobalTexture("_GlobalThemeTexture", theme.globalColorPalletTexture);
    }
}