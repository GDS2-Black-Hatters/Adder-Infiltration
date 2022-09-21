using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAdmin : MonoBehaviour
{
    [SerializeField] private AINode[] allAiNodes;

    [Header("Enemies")]
    [SerializeField] private Enemy[] availableEnemyPrefabs;
    [SerializeField] private int absoluteMaxEnemyCount;
    [field: SerializeField] public Transform EnemiesParent { get; private set; }

    [Header("Alert Status Light Color")]
    [SerializeField, Range(0, 1)] private float alertness = 0;
    [SerializeField, GradientUsage(true, ColorSpace.Gamma)] private Gradient worldAlertLightColor;
    [SerializeField, ColorUsage(false, true)] private Color fullAlertColor;
    [SerializeField] private float fullAlertColorLerpTime = 2;
    private float fullAlertColorLerp = 0;


    private void Update()
    {
        //if alertness is basically 1, lerp into full alert color, otherwise evaluate the gradient
        fullAlertColorLerp = Mathf.Clamp( fullAlertColorLerp + Time.deltaTime * (Mathf.Approximately(alertness, 1) ? 1 : -1), 0, fullAlertColorLerpTime);
        Shader.SetGlobalColor("_StatusEmissionColor", Color.Lerp( worldAlertLightColor.Evaluate(alertness), fullAlertColor, fullAlertColorLerp / fullAlertColorLerpTime));
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

    public void SpawnNewEnemy()
    {
        AINode startingNode = allAiNodes[Random.Range(0, allAiNodes.Length)];
        Enemy newEnemy = Instantiate(availableEnemyPrefabs[Random.Range(0, availableEnemyPrefabs.Length)], startingNode.transform.position + Vector3.up, Quaternion.identity);
    }
}
