using System.Collections;
using UnityEngine;

[RequireComponent(typeof(SphereCollider))]
public class Explosion : MonoBehaviour
{
    private SphereCollider sphereCollider;
    [SerializeField] private float radiusMaxSize;
    [Min(0.0001f),SerializeField] private float radiusExplosionOutTime = 0.5f;

    private void Awake()
    {
        sphereCollider = GetComponent<SphereCollider>();
    }

    private void OnEnable()
    {
        sphereCollider.enabled = true;
        StartCoroutine(ExplosionEffect());
    }

    private IEnumerator ExplosionEffect()
    {
        bool isRunning = true;
        TimeTracker time = new(radiusExplosionOutTime, 1);
        time.onFinish += () =>
        {
            isRunning = false;
            gameObject.SetActive(false);
        };

        do
        {
            yield return null;
            time.Update(Time.deltaTime);
            sphereCollider.radius = radiusMaxSize * time.TimePercentage;
        } while (isRunning);
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, radiusMaxSize);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Health playerHealth = GameManager.LevelManager.ActiveSceneController.playerHealth;
            playerHealth.ReduceHealth(playerHealth.health.originalValue * 0.5f);
            sphereCollider.enabled = false;
        }
    }
}
