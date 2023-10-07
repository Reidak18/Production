using UnityEngine;

namespace Production.GameLogic
{
    public class ResourceBuilding : Building
    {
        [HideInInspector]
        public bool isWorking { get; private set; }
        [HideInInspector]
        public Resource resource { get; private set; }

        public override void Init(BuildingDescription description, Vector3Int coords)
        {
            base.Init(description, coords);
            type = BuildingType.Resource;
            isWorking = false;
        }

        public void StartWorking(Resource resource)
        {
            if (resource == null)
            {
                Debug.LogError("Requested resource is null");
                return;
            }
            isWorking = true;
            this.resource = resource;
        }

        public override void StopWorking()
        {
            isWorking = false;
            resource = null;
        }

        public void SetResource(Resource resource)
        {
            this.resource = resource;
        }
    }
}
