using UnityEngine;

public class ProceduralWorldThemer : MonoBehaviour
{
    [SerializeField] private ThemeSetSO[] themes;
    [SerializeField] private bool changeTheme;

    private void Start()
    {
        ChangeTheme();
    }

    private void Update()
    {
        if (changeTheme)
        {
            ChangeTheme();
            changeTheme = false;
        }
    }

    private void ChangeTheme()
    {
        ThemeSetSO theme = themes[Random.Range(0, themes.Length)];
        Shader.SetGlobalTexture("_GlobalThemeTexture", theme.globalColorPalletTexture);
    }
}