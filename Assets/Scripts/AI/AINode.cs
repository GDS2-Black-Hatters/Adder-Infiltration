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
        AINode playerNode = levelManager.ActiveSceneController.GetClosestNode(levelManager.player);

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
