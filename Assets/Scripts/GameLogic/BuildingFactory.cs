using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Production.GameLogic
{
    public class BuildingFactory : MonoBehaviour
    {
        [SerializeField]
        private BuildingDescriptions descriptions;

        public ResourceBuilding CreateResourceBuilding(int index)
        {
            if (index >= descriptions.resourceBuildingsList.Count)
            {
                Debug.LogErrorFormat("Index of resource building is out of range. Use index in range 0..<{0}", descriptions.resourceBuildingsList.Count);
                return null;
            }

            GameObject buildingObject = new GameObject("ResourceBuilding");
            ResourceBuilding building = buildingObject.AddComponent<ResourceBuilding>();
            building.Init(descriptions.resourceBuildingsList[index]);
            building.transform.parent = transform;
            return building;
        }

        public ProcessingBuilding CreateProcessingBuilding(int index)
        {
            if (index >= descriptions.processingBuildingsList.Count)
            {
                Debug.LogErrorFormat("Index of processing building is out of range. Use index in range 0..<{0}", descriptions.processingBuildingsList.Count);
                return null;
            }

            GameObject buildingObject = new GameObject("ProcessingBuilding");
            ProcessingBuilding building = buildingObject.AddComponent<ProcessingBuilding>();
            building.Init(descriptions.processingBuildingsList[index]);
            building.transform.parent = transform;
            return building;
        }

        public MarketplaceBuilding CreateMarketplaceBuilding(int index)
        {
            if (index >= descriptions.marketplaceBuildingsList.Count)
            {
                Debug.LogErrorFormat("Index of marketplace building is out of range. Use index in range 0..<{0}", descriptions.marketplaceBuildingsList.Count);
                return null;
            }

            GameObject buildingObject = new GameObject("MarketplaceBuilding");
            MarketplaceBuilding building = buildingObject.AddComponent<MarketplaceBuilding>();
            building.Init(descriptions.marketplaceBuildingsList[index]);
            building.transform.parent = transform;
            return building;
        }
    }
}
