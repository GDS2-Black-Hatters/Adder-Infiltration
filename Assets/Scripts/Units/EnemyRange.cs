using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyRange : MonoBehaviour
{
    public GameObject enemy;


    // Start is called before the first frame update
    void Start()
    {
        enemy = transform.parent.gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Player")
        {
            enemy.GetComponent<Enemy>().ShootPlayer();
        }
    }
}
