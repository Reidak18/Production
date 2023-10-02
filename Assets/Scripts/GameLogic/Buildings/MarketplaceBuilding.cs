using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Production.GameLogic
{
    public class MarketplaceBuilding : Building
    {
        public override void Init(BuildingDescription description, Vector3Int coords)
        {
            base.Init(description, coords);
            type = BuildingType.Marketplace;
        }

        public override void StopWorking()
        {

        }
    }
}