using UnityEngine;

public abstract class Enemy : MonoBehaviour
{
    protected enum EnemyState
    {
        Patrol,
        Chase,
        Attack
    }
    protected EnemyState currentState = EnemyState.Patrol;
    public bool canAttack = false;

    protected virtual void Update()
    {
        switch (!GameManager.LevelManager.ActiveSceneController.InCaughtMode ? EnemyState.Patrol : !canAttack ? EnemyState.Chase : EnemyState.Attack)
        {
            case EnemyState.Patrol:
                Patrol();
                break;

            case EnemyState.Chase:
                Chase();
                break;

            case EnemyState.Attack:
                Attack();
                break;

            default:
                Debug.Log("How'd you even get here?");
                break;
        }
    }

    /// <summary>
    /// Meant to be overridden.
    /// </summary>
    protected virtual void Patrol() {}

    /// <summary>
    /// Meant to be overridden.
    /// </summary>
    protected virtual void Chase() {}

    /// <summary>
    /// Meant to be overridden.
    /// </summary>
    protected virtual void Attack()
    {
        //shootTimer += Time.deltaTime;
        //if (shootTimer >= 1.0f)
        //{
        //    Debug.Log("bang");
        //    Rigidbody bullet = Instantiate(bulletPrefab, gameObject.transform.position, gameObject.transform.rotation);
        //    Vector3 target = player.transform.position - bullet.transform.position;
        //    bullet.velocity = target * 3;
        //    shootTimer = 0;
        //}
    }
}
