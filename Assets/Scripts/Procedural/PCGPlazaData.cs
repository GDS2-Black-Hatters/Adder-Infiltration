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

    public override IEnumerator Generate(Transform rootParent, GameObject rootGameObject, MonoBehaviour generator, System.Action incompleteCall)
    {
        rootGameObject.name = "Plaza";
        Transform root = rootGameObject.transform;
        root.SetParent(rootParent);
        root.localPosition = Vector3.zero;

        BaseEnvironmentObject plazaCoreEnvObj = Instantiate(availablePlazaCorePrefab[Random.Range(0, availablePlazaCorePrefab.Length)], rootParent.position, Quaternion.identity);
        plazaCoreEnvObj.Initilize();
        plazaCoreEnvObj.transform.SetParent(root);
        plazaCoreEnvObj.transform.localPosition = Vector3.zero;

        if(availableEnvObjPrefab.Length <= 0) yield break; //return if no env obj is available for populating surrounding

        foreach(Vector3 offset in envObjPositionOffset)
        {
            BaseEnvironmentObject newEnvObj = Instantiate(availableEnvObjPrefab[Random.Range(0, availableEnvObjPrefab.Length)], root.position + offset * GlobalConst.chunkCellSizeUnitMultiplier/2, Quaternion.identity);
            newEnvObj.Initilize();
            newEnvObj.transform.SetParent(root);
        }

        yield break;
    }
}
