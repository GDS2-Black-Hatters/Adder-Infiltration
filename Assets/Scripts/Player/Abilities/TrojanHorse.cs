using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrojanHorse : AbilityBase
{
    [SerializeField] float abilityDuration = 10;
    [SerializeField] Mesh trojinMesh;

    private Mesh originalCoreMesh;

    private bool isAbilityActive = false;

    public override void ActivateAbility()
    {
        if(isAbilityActive) return;

        PlayerVirus pv = GameManager.LevelManager.player;
        pv.gameObject.layer = LayerMask.NameToLayer("Default");

        MeshFilter mf = pv.playerCoreObj.GetComponent<MeshFilter>();
        originalCoreMesh = mf.sharedMesh;
        mf.mesh = trojinMesh;

        isAbilityActive = true;
        StartCoroutine(DelayDisableAbility());
    }

    public override void EndAbilityPrime()
    {
        throw new System.NotImplementedException();
    }

    public override void StartAbilityPrime()
    {
        throw new System.NotImplementedException();
    }

    private IEnumerator DelayDisableAbility()
    {
        yield return new WaitForSeconds(abilityDuration);
        GameManager.LevelManager.player.gameObject.layer = LayerMask.NameToLayer("Player");
        GameManager.LevelManager.player.playerCoreObj.GetComponent<MeshFilter>().mesh = originalCoreMesh;
        isAbilityActive = false;
    }
}
