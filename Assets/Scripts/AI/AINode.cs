using System.Collections.Generic;
using UnityEngine;

public class AINode : MonoBehaviour
{
    [SerializeField] private AINode[] neighbours;

    public AINode GetRandomNeighbour()
    {
        return neighbours.Length > 0 ? neighbours[Random.Range(0, neighbours.Length)] : null;
    }

    public AINode GetNextNodeToPlayer()
    {
        LevelManager levelManager = GameManager.LevelManager;
        AINode playerNode = levelManager.ActiveSceneController.enemyAdmin.GetClosestNode(levelManager.player.transform);

        AINode nextNode = null;
        float shortestDist = int.MaxValue;
        foreach (AINode neighbour in neighbours)
        {
            float dist = (playerNode.transform.position - neighbour.transform.position).sqrMagnitude;
            if (shortestDist > dist)
            {
                nextNode = neighbour;
                shortestDist = dist;
            }
        }

        return nextNode;
    }

    public void AddNeighbour(AINode neighbour)
    {
        List<AINode> newNeighbourList = new(this.neighbours);
        if(neighbour != null && !newNeighbourList.Contains(neighbour))
        {
            newNeighbourList.Add(neighbour);
        }
        this.neighbours = newNeighbourList.ToArray();
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, 1f);
    }

    private void OnDrawGizmosSelected()
    {
        foreach (AINode neighbour in neighbours)
        {
            if (neighbour) //To stop editor complaints.
            {
                Gizmos.DrawLine(transform.position, neighbour.transform.position);
            }
        }
    }
}
