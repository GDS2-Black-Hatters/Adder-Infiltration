using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AbilityBase : MonoBehaviour
{
    public abstract void StartAbilityPrime();
    public abstract void EndAbilityPrime();
    public abstract void ActivateAbility();
}
