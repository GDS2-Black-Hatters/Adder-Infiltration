using UnityEngine;

/// <summary>
/// Simple attack range for the enemy.
/// Best to attach this to a child GameObject with a collider.
/// </summary>
public class EnemyAttackRange : MonoBehaviour
{
    private Enemy enemy;

    void Awake()
    {
        enemy = GetComponentInParent<Enemy>(); //Expected to exist.
    }

    private void OnTriggerEnter(Collider other)
    {
        enemy.canAttack = other.CompareTag("Player") || enemy.canAttack;
    }

    private void OnTriggerExit(Collider other)
    {
        enemy.canAttack = !other.CompareTag("Player") && enemy.canAttack;
    }
}
