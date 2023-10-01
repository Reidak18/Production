using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Production.GameLogic
{
    public class ProcessingBuilding : Building
    {
        [HideInInspector]
        public bool isWorking;
        [HideInInspector]
        public Resource resource1;
        [HideInInspector]
        public Resource resource2;
        [HideInInspector]
        public Product product;

        public override void Init(BuildingDescription description, Vector3Int coords)
        {
            base.Init(description, coords);
            type = BuildingType.Processing;
            isWorking = false;
        }

        public void SetResources(Resource resource1, Resource resource2)
        {
            this.resource1 = resource1;
            this.resource2 = resource2;
        }
    }
}
