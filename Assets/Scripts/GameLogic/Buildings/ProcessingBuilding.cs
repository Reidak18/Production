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
        public bool isWorking { get; private set; }
        [HideInInspector]
        public Resource[] resources { get; private set; }
        [HideInInspector]
        public Product product { get; private set; }

        // нужно для отслеживания вынужденных остановок из-за нехватки ресурсов
        public event Action OnWorkingStopped;

        public override void Init(BuildingDescription description, Vector3Int coords)
        {
            base.Init(description, coords);
            type = BuildingType.Processing;
            isWorking = false;
            resources = new Resource[2];
        }

        public void StartWorking(Resource[] resources, Product product)
        {
            if (resources == null || product == null)
            {
                Debug.LogError("Requested product or recyclable resources are null");
                return;
            }
            isWorking = true;
            this.resources = resources;
            this.product = product;
        }

        public override void StopWorking()
        {
            isWorking = false;
            resources = new Resource[2];
            product = null;
            OnWorkingStopped?.Invoke();
        }

        public void SetResources(Resource[] resources)
        {
            this.resources = resources;
        }
    }
}
