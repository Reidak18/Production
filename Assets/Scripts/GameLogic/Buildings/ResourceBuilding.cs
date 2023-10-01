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

        public override void Init(BuildingDescription description, Vector3Int coords)
        {
            base.Init(description, coords);
            type = BuildingType.Resource;
            isWorking = false;
        }

        public void SetResource(Resource resource)
        {
            this.resource = resource;
        }
    }
}
