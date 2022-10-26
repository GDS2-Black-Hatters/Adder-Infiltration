using System;
using UnityEngine;

/// <summary>
/// Simple attack range for the enemy.
/// Best to attach this to a child GameObject with a collider.
/// </summary>
public class EnemyAttackRange : MonoBehaviour
{
    private Enemy enemy;
    private Action<bool> WithinAttackRange;

    void Awake()
    {
        enemy = GetComponentInParent<Enemy>(); //Expected to exist.
    }

    public void AddTrigger(Action<bool> trigger)
    {
        WithinAttackRange = trigger;
    }

    private void OnTriggerEnter(Collider other)
    {
        WithinAttackRange?.Invoke(other.CompareTag("Player") || enemy.CanAttack);
    }

    private void OnTriggerExit(Collider other)
    {
        WithinAttackRange?.Invoke(!other.CompareTag("Player") && enemy.CanAttack);
    }
}
