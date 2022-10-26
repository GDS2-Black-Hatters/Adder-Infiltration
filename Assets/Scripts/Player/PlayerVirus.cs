using System;
using UnityEngine;
using static LevelManager.Level;

public class PlayerVirus : MonoBehaviour
{
    [field: SerializeField] public PlayerVirusController VirusController { get; private set; }
    [field: SerializeField] public GameObject PlayerVisual { get; private set; }
    [field: SerializeField] public Health HP { get; private set; } = new(20);
    public bool IsProtected { get; private set; } = false;
    private PlayerVirusMoveControl movement;

    private void Awake()
    {
        movement = GetComponent<PlayerVirusMoveControl>();
        HP.onDeath += () =>
        {
            GameManager.LevelManager.ChangeLevel(Hub);
        };
    }

    public void Dash(float strength)
    {
        movement.Dash(strength);
    }

    public void Heal(float percentage)
    {
        HP.ReduceHealth(HP.health.originalValue * -percentage);
    }

    public void Damage(float amount)
    {
        if (!IsProtected)
        {
            HP.ReduceHealth(amount);
        }
    }

    public void DamagePercentage(float percentage)
    {
        if (!IsProtected)
        {
            HP.ReduceHealth(HP.health.originalValue * percentage);
        }
    }

    public void SetProtected(bool protection)
    {
        IsProtected = protection;
    }
}
