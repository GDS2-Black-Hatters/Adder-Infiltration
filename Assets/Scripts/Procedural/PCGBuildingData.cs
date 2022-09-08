using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObject/ProceduralBuilding", fileName = "NewProceduralBuilding")]
public class PCGBuildingData : ScriptableObject
{
    [Header("Generation Settings")]
    public int MinStep = 2;
    public int MaxStep = 5;

    [Header("Modules")]
    public PCGBuildingModule[] BaseModules;
    public PCGBuildingModule[] MidModules;
    public PCGBuildingModule[] EndModules;
}
