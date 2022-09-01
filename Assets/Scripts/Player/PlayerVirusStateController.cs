using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerVirusStateController : MonoBehaviour
{
    enum PlayerState {
        stealth,
        alert,
    }

    private PlayerState currentPlayerState;

    [SerializeField] MatterShell matterShell;
    [SerializeField] RandRotate geoShellRotator;

    [Header("Transformation")]
    [SerializeField] private float matterShellTransformSize = 0.75f;
    [SerializeField] private float geometricShellTransformSize = 1.2f;
    [SerializeField] private float geoShellRotationSpeedChange = 3f;
    [SerializeField] private float transformShellStartDelay = 1;
    [SerializeField] private float transformShellEffectDuration = 3;

    void Start()
    {
        currentPlayerState = PlayerState.stealth;
        GetComponent<UnityEngine.VFX.VisualEffect>().Stop();

        LevelSceneController lsc = (LevelSceneController)GameManager.LevelManager.ActiveSceneController;
        lsc.OnPlayerDetection.AddListener(EngageAlertMode); //TO CHANGE
    }

    public void EngageAlertMode()
    {
        currentPlayerState = PlayerState.alert;
        StartCoroutine(PlayConversionEffect());
    }

    private IEnumerator PlayConversionEffect()
    {
        float curTime = 0;
        UnityEngine.VFX.VisualEffect convertEffect = GetComponent<UnityEngine.VFX.VisualEffect>();

        Lerper matterShellSizeLerp = new();
        matterShellSizeLerp.SetValues(1, matterShellTransformSize, transformShellStartDelay);
        Vector3 initialMatterShellSize = matterShell.transform.localScale;
        
        Lerper geometricShellSizeLerp = new();
        geometricShellSizeLerp.SetValues(1, geometricShellTransformSize, transformShellStartDelay);
        Vector3 initialGeoShellSize = geoShellRotator.transform.localScale;

        Lerper geometricShellRotationSpeedLerp = new();
        geometricShellRotationSpeedLerp.SetValues(1, geoShellRotationSpeedChange, transformShellStartDelay);
        float initialGeoShellRotSpeed = geoShellRotator.rotationSpeed;

        //Start Lerping Matter and GeoShell size and delay TransformShell Start
        while(curTime < transformShellStartDelay)
        {
            yield return null;

            matterShellSizeLerp.Update(Time.deltaTime);
            matterShell.transform.localScale = initialMatterShellSize * matterShellSizeLerp.currentValue;

            geometricShellSizeLerp.Update(Time.deltaTime);
            geoShellRotator.transform.localScale = initialGeoShellSize * geometricShellSizeLerp.currentValue;

            geometricShellRotationSpeedLerp.Update(Time.deltaTime);
            geoShellRotator.rotationSpeed = initialGeoShellRotSpeed * geometricShellRotationSpeedLerp.currentValue;

            curTime += Time.deltaTime;
        }
        curTime -= transformShellStartDelay;

        //Start Transform Shell Effect
        convertEffect.Play();

        //Wait for Half the Transform Duration
        while(curTime < transformShellEffectDuration/2)
        {
            yield return null;
            curTime += Time.deltaTime;
        }
        //Weaponize Matter Shell, then proceed with waiting for the remaining half of the transformation duration
        matterShell.WeaponizeMatter();
        while(curTime < transformShellEffectDuration)
        {
            yield return null;
            curTime += Time.deltaTime;
        }
        curTime -= transformShellEffectDuration;

        //End Transform Shell Effect
        convertEffect.Stop();

        matterShellSizeLerp.SetValues(matterShellTransformSize, 1, transformShellStartDelay);
        geometricShellSizeLerp.SetValues(geometricShellTransformSize, 1, transformShellStartDelay);
        geometricShellRotationSpeedLerp.SetValues(geoShellRotationSpeedChange, 1, transformShellStartDelay);

        //Reset Geo and Matter Shell Size
        while(curTime < transformShellStartDelay)
        {
            yield return null;

            matterShellSizeLerp.Update(Time.deltaTime);
            matterShell.transform.localScale = initialMatterShellSize * matterShellSizeLerp.currentValue;

            geometricShellSizeLerp.Update(Time.deltaTime);
            geoShellRotator.transform.localScale = initialGeoShellSize * geometricShellSizeLerp.currentValue;
            
            geometricShellRotationSpeedLerp.Update(Time.deltaTime);
            geoShellRotator.rotationSpeed = initialGeoShellRotSpeed * geometricShellRotationSpeedLerp.currentValue;

            curTime += Time.deltaTime;
        }
    }
}
