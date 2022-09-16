using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObject/Block/ProceduralPlaza", fileName = "NewProceduralPlaza")]
public class PCGPlazaData : PCGBlockScriptable
{
    [SerializeField] protected Vector2Int _blockSize = Vector2Int.one;
    public override Vector2Int blockSize { get { return _blockSize; }}

    [SerializeField] private BaseEnvironmentObject[] availablePlazaCorePrefab;
    [SerializeField] private BaseEnvironmentObject[] availableEnvObjPrefab;
    [SerializeField] private Vector3[] envObjPositionOffset;

    public override GameObject Generate(Transform rootParent, float cellUnitMultiplier)
    {
        Transform root = new GameObject("Plaza").transform;
        root.SetParent(rootParent);
        root.localPosition = Vector3.zero;

        Transform plazaCore = Instantiate(availablePlazaCorePrefab[Random.Range(0, availablePlazaCorePrefab.Length)], rootParent.position, Quaternion.identity).transform;
        plazaCore.SetParent(root);
        plazaCore.localPosition = Vector3.zero;

        if(availableEnvObjPrefab.Length <= 0) return root.gameObject; //return if no env obj is available for populating surrounding

        foreach(Vector3 offset in envObjPositionOffset)
        {
            Transform newEnvObj = Instantiate(availableEnvObjPrefab[Random.Range(0, availableEnvObjPrefab.Length)], root.position + offset * cellUnitMultiplier/2, Quaternion.identity).transform;
            newEnvObj.SetParent(root);
        }

        return root.gameObject;
    }
}
