using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PCGBuildingGenerator : MonoBehaviour
{
    [SerializeField] private PCGBuildingData BuildingGenData;

    [SerializeField] private bool testGenerate;

    private void GenerateBuilding()
    {
        int Steps = Random.Range(BuildingGenData.MinStep, BuildingGenData.MaxStep);

        PCGBuildingModule module;
        Vector3 attachPointPosition = transform.position;

        ref PCGBuildingModule[] arrayToGen = ref (BuildingGenData.BaseModules.Length != 0) ? ref BuildingGenData.BaseModules : ref BuildingGenData.MidModules;

        //First Step, Generate Base
        module = Instantiate(arrayToGen[Random.Range(0, arrayToGen.Length)], transform.position, Quaternion.identity, transform);
        attachPointPosition = module.attatchPoint + module.transform.position;

        //Mid Steps
        arrayToGen = ref BuildingGenData.MidModules;
        for(int i = 1; i < Steps - 1; i++)
        {
            module = Instantiate(arrayToGen[Random.Range(0, arrayToGen.Length)], attachPointPosition, Quaternion.identity, transform);
            attachPointPosition = module.attatchPoint + module.transform.position;
        }

        //End Step
        arrayToGen = ref (BuildingGenData.EndModules.Length != 0) ? ref BuildingGenData.EndModules : ref BuildingGenData.MidModules;
        module = Instantiate(arrayToGen[Random.Range(0, arrayToGen.Length)], attachPointPosition, Quaternion.identity, transform);
        attachPointPosition = module.attatchPoint + module.transform.position;
    }

    private void OnValidate()
    {
        if(testGenerate)
        {
            testGenerate = false;
            GenerateBuilding();
        }
    }
}
