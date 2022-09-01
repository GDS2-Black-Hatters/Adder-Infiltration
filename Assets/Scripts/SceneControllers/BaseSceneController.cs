using System;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseSceneController : MonoBehaviour
{
    public enum SceneState
    {
        Stealth,
        Combat,
    }
    public SceneState sceneMode { get; private set; } = SceneState.Stealth;

    //For children.
    protected Dictionary<string, bool> mandatoryObjectives = new();
    protected Dictionary<string, bool> optionalObjectives = new();
    protected event Action onPlayerDetection;

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
        if (sceneMode == SceneState.Stealth)
        {
            sceneMode = SceneState.Combat;
            onPlayerDetection?.Invoke();
            lerper.SetValues(0, 1, lerpTime);
            if (matterShell)
            {
                matterShell.WeaponizeMatter();
            }
        }
    }

    public void SetMatterShell(MatterShell shell)
    {
        matterShell = shell;
    }
}
