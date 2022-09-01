#pragma warning disable IDE1006 // Naming Styles
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseSceneController : MonoBehaviour
{
    [field: SerializeField] public bool InCaughtMode { get; private set; } = false;

    //For children.
    protected Dictionary<string, bool> mandatoryObjectives = new();
    protected Dictionary<string, bool> optionalObjectives = new();
    protected DoStatic.SimpleDelegate onPlayerDetection;

    //Skybox Lerping.
    [SerializeField] private Color startColor = Color.green;
    [SerializeField] private Color endColor = Color.red;
    [SerializeField] private float lerpTime = 1f;
    private readonly Lerper lerper = new();

    //MatterShell
    private MatterShell matterShell;

    protected virtual void Awake()
    {
        GameManager.LevelManager.SetActiveSceneController(this);

        //Create and assign a copy so we don't change the asset original values
        RenderSettings.skybox = new(RenderSettings.skybox);
        RenderSettings.skybox.SetColor("_SkyTint", startColor); //Might be unecessary.
    }

    protected virtual void Update()
    {
        if (lerper.isLerping)
        {
            lerper.Update(Time.deltaTime);
            RenderSettings.skybox.SetColor("_SkyTint", Color.Lerp(startColor, endColor, lerper.currentValue));
        }
    }

    public void StartCaughtMode()
    {
        if (!InCaughtMode)
        {
            InCaughtMode = true;
            onPlayerDetection?.Invoke();
            lerper.SetValues(0, 1, lerpTime);
            if (matterShell)
            {
                matterShell.WeaponizeMatter(); //Should be removed, matter shell converstion now handled by player virus state controller
            }
        }
    }

    public void SetMatterShell(MatterShell shell)
    {
        matterShell = shell;
    }
}
