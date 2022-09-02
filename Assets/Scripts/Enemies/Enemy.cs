using UnityEngine;

/// <summary>
/// Basic abstract class of the enemy script behaviour.
/// </summary>
public abstract class Enemy : MonoBehaviour
{
    /// <summary>
    /// All generic enemies have three AI states:
    /// <br></br>
    /// <br>Patrol</br>
    /// <br> - For when the player has yet to be seen</br>
    /// <br> - The enemy should just patrol their given route or something.</br>
    /// <br></br>
    /// <br>Chase</br>
    /// <br> - For when the player has been seen. When seen all enemies know the player's position regardless if they have seen them or not.</br>
    /// <br></br>
    /// <br>Attack</br>
    /// <br> - For when the player is in their attack range.</br>
    /// </summary>
    protected enum EnemyState
    {
        Patrol,
        Chase,
        Attack
    }
    protected EnemyState currentState = EnemyState.Patrol;
    public bool canAttack = false;

    /// <summary>
    /// Should be overridden ONLY when the enemy has their own enum states.
    /// </summary>
    protected virtual void Update()
    {
        switch (GameManager.LevelManager.ActiveSceneController.sceneMode == BaseSceneController.SceneState.Stealth ? EnemyState.Patrol : !canAttack ? EnemyState.Chase : EnemyState.Attack)
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
    /// This is for the Patrol behaviour.
    /// </summary>
    protected virtual void Patrol() { }

    /// <summary>
    /// Meant to be overridden.
    /// This is for the Chase behaviour.
    /// </summary>
    protected virtual void Chase() { }

    /// <summary>
    /// Meant to be overridden.
    /// This is for the Attack behaviour.
    /// </summary>
    protected virtual void Attack() { }
}
