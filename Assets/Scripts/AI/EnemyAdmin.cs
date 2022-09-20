using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAdmin : MonoBehaviour
{
    [SerializeField] private AINode[] allAiNodes;

    public void NewAiNodes(AINode[] newAiNodes)
    {
        allAiNodes = newAiNodes;
    }
}
