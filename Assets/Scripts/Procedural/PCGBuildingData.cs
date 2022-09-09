using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObject/ProceduralBuilding", fileName = "NewProceduralBuilding")]
public class PCGBuildingData : PCGBlockScriptable
{
    [Header("Generation Settings")]
    public int MinStep = 2;
    public int MaxStep = 5;

    [Header("Modules")]
    public PCGBuildingModule[] BaseModules;
    public PCGBuildingModule[] MidModules;
    public PCGBuildingModule[] CapModules;

    public override GameObject Generate(Transform rootParent = null)
    {
        Transform root = new GameObject("BuildingRoot").transform;
        if(rootParent != null) root.SetParent(rootParent);
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
            attachPointPosition = module.attatchPoint + module.transform.position;
        }

        //End Step
        arrayToGen = ref (CapModules.Length != 0) ? ref CapModules : ref MidModules;
        module = Instantiate(arrayToGen[Random.Range(0, arrayToGen.Length)], attachPointPosition, Quaternion.identity, root);
        attachPointPosition = module.attatchPoint + module.transform.position;

        return root.gameObject;
    }
}
