using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PCGGround : MonoBehaviour
{
    [SerializeField] private GameObject FloorGroundPrefab;
    [SerializeField] private GameObject DistGroundLayerPrefab;

    [SerializeField] private int Layers = 3;
    [SerializeField] private float LayerDist = 6;

    private void Start()
    {
        //GameObject ground = Instantiate(FloorGroundPrefab, Vector3.zero, Quaternion.identity, transform);
        //ground.transform.localScale = Vector3.one;

        for(int i = 0; i < Layers; i++)
        {
            GameObject newLayer = Instantiate(DistGroundLayerPrefab, new Vector3(transform.position.x, (i + 1) * -LayerDist, transform.position.z), Quaternion.identity, transform);
            newLayer.transform.localScale = Vector3.one;
        }
    }

}
