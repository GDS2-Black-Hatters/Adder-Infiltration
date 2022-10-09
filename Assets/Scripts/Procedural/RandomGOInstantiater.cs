using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomGOInstantiater : MonoBehaviour
{
    [SerializeField, Range(0,1)] private float spawnChance = 0.5f;
    [SerializeField] private GameObject[] possibleObjects;

    private void Start()
    {
        if(DoStatic.RandomBool(spawnChance))
        {
            Instantiate(possibleObjects[Random.Range(0, possibleObjects.Length)], transform.position, transform.rotation, transform);
        }
        Destroy(this);
    }
}
