using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Production.GameLogic
{
    public class MarketplaceBuilding : Building
    {
        [HideInInspector]
        public Product product;

        public override void Init(BuildingDescription description, Vector3Int coords)
        {
            base.Init(description, coords);
            type = BuildingType.Marketplace;
        }

        public void SetProduct(Product product)
        {
            this.product = product;
        }
    }
}