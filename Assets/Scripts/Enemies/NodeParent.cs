using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NodeParent : MonoBehaviour
{
    public GameObject enemyPatrol;
    int patrolNum = 0;
    private void Awake()
    {
        Transform[] nodes = GetComponentsInChildren<Transform>();


        foreach (Transform node in nodes)
        {
            GameObject patrol = Instantiate(enemyPatrol, node.transform.position, node.transform.rotation);
            patrol.name = "Enemy Patrol " + patrolNum;
            patrolNum++;
        }
        Destroy(GameObject.Find("Enemy Patrol 0"));
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
