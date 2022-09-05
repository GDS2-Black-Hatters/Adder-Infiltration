using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NodeParent : MonoBehaviour
{
    [SerializeField] private GameObject enemyPatrol;
    [SerializeField] private int spawnLimiter = 1;


    public Transform[] nodes;
    private void Awake()
    {
        nodes = GetComponentsInChildren<Transform>();

        //Applied quick patch fix, THIS SHOULD BE CHANGED
        for (int i = 0; i < Mathf.Min(nodes.Length-1, spawnLimiter); i++)
        {
            if(nodes[i] == this)
            {
                continue;
            }

            Shooter patrol = Instantiate(enemyPatrol, nodes[i].transform.position, nodes[i].transform.rotation, transform).GetComponent<Shooter>();
            patrol.name = "Enemy Patrol " + i;

            patrol.currentNode = i;
            patrol.SetNodeParent(this);
        }
    }
}
