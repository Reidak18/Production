using System.Collections;
using System.Collections.Generic;
using Production.GameLogic;
using UnityEngine;

namespace Production.UI
{
    public class GameState : State
    {
        [SerializeField]
        private GameStateView view;

        public void InitLootUIs(List<Loot> loots)
        {
            view.InitLootUIs(loots);
        }

        public void UpdateWarehouseContent(string lootId, int quantity)
        {
            view.UpdateQuantity(lootId, quantity);
        }

        public void ReloadWarehouseContent(Dictionary<string, int> loots)
        {
            view.ReloadAllQuantity(loots);
        }
    }
}
