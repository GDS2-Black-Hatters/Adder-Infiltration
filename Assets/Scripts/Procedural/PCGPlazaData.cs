using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObject/Block/ProceduralPlaza", fileName = "NewProceduralPlaza")]
public class PCGPlazaData : PCGBlockScriptable
{
    [SerializeField] protected Vector2Int _blockSize = Vector2Int.one;
    public override Vector2Int blockSize { get { return _blockSize; }}

    [SerializeField] private GameObject[] AvailablePlazaCorePrefab;

    public override GameObject Generate(Transform rootParent)
    {
        Transform root = Instantiate(AvailablePlazaCorePrefab[Random.Range(0, AvailablePlazaCorePrefab.Length)], rootParent.position, Quaternion.identity).transform;
        root.SetParent(rootParent);
        root.localPosition = Vector3.zero;

        return root.gameObject;
    }
}
