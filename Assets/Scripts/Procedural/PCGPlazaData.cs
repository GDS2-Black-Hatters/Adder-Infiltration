using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObject/Block/ProceduralPlaza", fileName = "NewProceduralPlaza")]
public class PCGPlazaData : PCGBlockScriptable
{
    [SerializeField] protected Vector2Int _blockSize = Vector2Int.one;
    public override Vector2Int blockSize { get { return _blockSize; }}

    [System.Flags]
    private enum ObjectScale
    { small = 1, medium = 2, large = 4}

    [SerializeField] private GameObject PlazaCorePrefab;
    [SerializeField] private ObjectScale plazaScale;

    public override GameObject Generate(Transform rootParent)
    {
        Transform root = Instantiate(PlazaCorePrefab, rootParent.position, Quaternion.identity).transform;
        root.SetParent(rootParent);
        root.localPosition = Vector3.zero;

        return root.gameObject;
    }
}
