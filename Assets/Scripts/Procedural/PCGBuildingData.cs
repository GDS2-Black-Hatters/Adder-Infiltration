using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObject/Block/ProceduralBuilding", fileName = "NewProceduralBuilding")]
public class PCGBuildingData : PCGBlockScriptable
{
    [SerializeField] protected Vector2Int _blockSize = Vector2Int.one;
    public override Vector2Int blockSize { get { return _blockSize; }}

    [Header("Generation Height Settings")]
    [SerializeField] private int MinStep = 2;
    [SerializeField] private int MaxStep = 5;

    [Header("Generation Offset Settings")]
    [SerializeField] private float ConstantExtraRotation = 0;
    [SerializeField] private bool Random90DegreeRot = false;

    [Header("Modules")]
    [SerializeField] private PCGBuildingModule[] BaseModules;
    [SerializeField] private PCGBuildingModule[] MidModules;
    [SerializeField] private PCGBuildingModule[] CapModules;

    public override GameObject Generate(Transform rootParent)
    {
        Transform root = new GameObject("BuildingRoot").transform;
        root.SetParent(rootParent);
        root.localPosition = Vector3.zero;

        int Steps = Random.Range(MinStep, MaxStep);

        PCGBuildingModule module;
        Vector3 attachPointPosition = root.position;

        ref PCGBuildingModule[] arrayToGen = ref (BaseModules.Length != 0) ? ref BaseModules : ref MidModules;

        //First Step, Generate Base
        module = Instantiate(arrayToGen[Random.Range(0, arrayToGen.Length)], root.position, Quaternion.identity, root);
        attachPointPosition = module.attatchPoint + module.transform.position;

        //Mid Steps
        arrayToGen = ref MidModules;
        for(int i = 1; i < Steps - 1; i++)
        {
            module = Instantiate(arrayToGen[Random.Range(0, arrayToGen.Length)], attachPointPosition, Quaternion.identity, root);
            module.transform.rotation = Quaternion.Euler(0, (i * ConstantExtraRotation) + (Random.Range(0, 4) * (Random90DegreeRot ? 90 : 0)), 0);
            attachPointPosition = module.attatchPoint + module.transform.position;
        }

        //End Step
        arrayToGen = ref (CapModules.Length != 0) ? ref CapModules : ref MidModules;
        module = Instantiate(arrayToGen[Random.Range(0, arrayToGen.Length)], attachPointPosition, Quaternion.identity, root);
        module.transform.rotation = Quaternion.Euler(0, ((Steps - 1) * ConstantExtraRotation) + (Random.Range(0, 4) * (Random90DegreeRot ? 90 : 0)), 0);
        attachPointPosition = module.attatchPoint + module.transform.position;

        return root.gameObject;
    }
}
