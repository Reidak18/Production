using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Production.GameLogic
{
    [Serializable]
    public class BuildingDescription
    {
        public int productionTime;
    }

    [CreateAssetMenu(fileName = "BuildingDescriptions", menuName = "Building descriptions")]
    public class BuildingDescriptions : ScriptableObject
    {
        [SerializeField]
        public List<BuildingDescription> resourceBuildingsList;
        [SerializeField]
        public List<BuildingDescription> processingBuildingsList;
        [SerializeField]
        public List<BuildingDescription> marketplaceBuildingsList;
    }
}