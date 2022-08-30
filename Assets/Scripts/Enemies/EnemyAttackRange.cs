using UnityEngine;

/// <summary>
/// Simple attack range for the enemy.
/// Best to attach this to a child GameObject with a collider.
/// </summary>
public class EnemyAttackRange : MonoBehaviour
{
    private Enemy enemy;

    void Start()
    {
        enemy = GetComponentInParent<Enemy>(); //Expected to exist.
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            enemy.canAttack = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            enemy.canAttack = false;
        }
    }
}
