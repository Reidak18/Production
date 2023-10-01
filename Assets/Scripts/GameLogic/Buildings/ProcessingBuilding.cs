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
        public Resource[] resources = new Resource[2];
        [HideInInspector]
        public Product product;

        public override void Init(BuildingDescription description, Vector3Int coords)
        {
            base.Init(description, coords);
            type = BuildingType.Processing;
            isWorking = false;
        }

        public void SetResources(Resource[] resources)
        {
            this.resources = resources;
        }
    }
}
