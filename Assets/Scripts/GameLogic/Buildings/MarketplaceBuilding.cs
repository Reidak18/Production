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

        public void Init(BuildingDescription description)
        {
            type = BuildingType.Marketplace;
            this.description = description;
        }

        public void SetProduct(Product product)
        {
            this.product = product;
        }

        public override void StartWorking(Func<Dictionary<Loot, int>, bool> getFromProvider, Action<Dictionary<Loot, int>> sendToConsumer)
        {

        }
    }
}