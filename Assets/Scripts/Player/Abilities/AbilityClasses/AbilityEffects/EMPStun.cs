using UnityEngine;

public class EMPStun : MonoBehaviour
{
    private EMP emp;

    private void Start()
    {
        emp = GetComponentInParent<EMP>();
    }

    private void OnTriggerEnter(Collider other)
    {
        Enemy enemy;
        if (other.gameObject.layer == LayerMask.NameToLayer("EnemyUnit") && (enemy = other.GetComponentInParent<Enemy>()))
        {
            enemy.StartStun(emp.GetStunDuration());
        }
    }
}
