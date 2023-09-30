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

        public void Init(BuildingDescription description)
        {
            type = BuildingType.Processing;
            this.description = description;
            isWorking = false;
        }

        public void SetResources(Resource resource1, Resource resource2)
        {
            this.resource1 = resource1;
            this.resource2 = resource2;
        }

        public override void StartWorking(Func<Dictionary<Loot, int>, bool> getFromProvider, Action<Dictionary<Loot, int>> sendToConsumer)
        {

        }
    }
}
