using System.Collections;
using UnityEngine;

public class EnemyAdmin : MonoBehaviour
{
    [SerializeField] private GameObject aiNodes;
    private AINode[] allAiNodes;
    public bool IsDisguised = false;

    [Header("Enemies")]
    [SerializeField] private Enemy[] availableEnemyPrefabs;
    [SerializeField] private int absoluteMaxEnemyCount;
    [SerializeField] private float playerSpawnEnemyFreeBufferRadius = 200;

    [Header("Alert Status Light Color")]
    [SerializeField, Range(0, 1)] private float alertness = 0;
    [SerializeField, GradientUsage(true, ColorSpace.Gamma)] private Gradient worldAlertLightColor;
    [SerializeField, ColorUsage(false, true)] private Color fullAlertColor;
    [SerializeField] private float fullAlertColorLerpTime = 2;
    private float fullAlertColorLerp = 0;

    public event System.Action OnFullAlert;
    public bool FullAlertTriggered { get; private set; } = false;

    private void Awake()
    {
        allAiNodes = aiNodes.GetComponentsInChildren<AINode>();
    }

    private void Update()
    {
        //if alertness is basically 1, lerp into full alert color, otherwise evaluate the gradient
        fullAlertColorLerp = Mathf.Clamp(fullAlertColorLerp + Time.deltaTime * (Mathf.Approximately(alertness, 1) ? 1 : -1), 0, fullAlertColorLerpTime);
        Shader.SetGlobalColor("_StatusEmissionColor", Color.Lerp(worldAlertLightColor.Evaluate(alertness), fullAlertColor, fullAlertColorLerp / fullAlertColorLerpTime));
    }

    public void IncreaseAlertness(float delta)
    {
        if (!IsDisguised)
        {
            StartCoroutine(LerpIncreaseAlertness(delta));
        }
    }

    private IEnumerator LerpIncreaseAlertness(float delta, float increasePeriod = 0.5f)
    {
        float increasedAmount = 0;
        do
        {
            if (FullAlertTriggered)
            {
                break; //To prevent double full alert trigger.
            }

            float change = Mathf.Min(Time.deltaTime / increasePeriod * delta, delta - increasedAmount);
            alertness += change;
            increasedAmount += change;

            if (alertness >= 1)
            {
                OnFullAlert?.Invoke();
                FullAlertTriggered = true;
            }

            yield return null;
        } while (increasedAmount < delta);
    }

    public void NewAiNodes(AINode[] newAiNodes)
    {
        allAiNodes = newAiNodes;
    }

    public AINode GetClosestNode(Transform target)
    {
        AINode closest = null;
        float closestDist = int.MaxValue;
        foreach (AINode node in allAiNodes)
        {
            float nodeDist = (node.transform.position - target.position).sqrMagnitude;
            if (closestDist > nodeDist)
            {
                closest = node;
                closestDist = nodeDist;
            }
        }
        return closest;
    }

    public void SpawnEnemies(int count)
    {
        for (int i = 0; i < count; i++)
        {
            SpawnNewEnemy();
        }
    }

    public void SpawnNewEnemy()
    {
        AINode startingNode;
        do //do a radius check so we don't spawn a damn shark right next to the player
        {
            startingNode = allAiNodes[Random.Range(0, allAiNodes.Length)];
        } while (Vector3.Distance(GameManager.LevelManager.ActiveSceneController.Player.transform.position, startingNode.transform.position) < playerSpawnEnemyFreeBufferRadius);
        Instantiate(availableEnemyPrefabs[Random.Range(0, availableEnemyPrefabs.Length)], startingNode.transform.position + Vector3.up, Quaternion.identity);
    }
}
