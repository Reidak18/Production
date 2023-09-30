using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Production.GameLogic
{
    public class ResourceBuilding : Building
    {
        [HideInInspector]
        public bool isWorking;
        [HideInInspector]
        public Resource resource;

        public void Init(BuildingDescription description)
        {
            type = BuildingType.Resource;
            this.description = description;
            isWorking = false;
        }

        public void SetResource(Resource resource)
        {
            this.resource = resource;
        }

        public override void StartWorking(Func<Dictionary<Loot, int>, bool> getFromProvider, Action<Dictionary<Loot, int>> sendToConsumer)
        {

        }
    }
}
