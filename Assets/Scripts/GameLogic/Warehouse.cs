using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Production.GameLogic
{
    public class Warehouse
    {
        public Dictionary<string, int> loots = new Dictionary<string, int>();

        public event Action<string, int> OnWarehouseContentChanged;
        public event Action<Dictionary<string, int>> OnWarehouseContentLoaded;

        public void Load(LootDescriptions lootDescriptions)
        {
            loots.Clear();

            foreach (var resource in lootDescriptions.resourcesList)
            {
                loots.Add(resource.id, PlayerPrefs.GetInt(resource.id, 0));
            }
            foreach (var product in lootDescriptions.productsList)
            {
                loots.Add(product.id, PlayerPrefs.GetInt(product.id, 0));
            }

            OnWarehouseContentLoaded?.Invoke(loots);
        }

        public void Save()
        {
            foreach (var lootId in loots.Keys)
            {
                PlayerPrefs.SetInt(lootId, loots[lootId]);
            }
        }

        public bool GetFromWarehouse(Dictionary<string, int> required)
        {
            foreach(var lootId in required.Keys)
            {
                if (!loots.TryGetValue(lootId, out int quantity))
                    return false;

                if (quantity < required[lootId])
                    return false;
            }

            foreach (var lootId in required.Keys)
            {
                loots[lootId] -= required[lootId];
                OnWarehouseContentChanged?.Invoke(lootId, loots[lootId]);
            }

            return true;
        }

        public void AddToWarehouse(string lootId, int quantity)
        {
            if (!loots.ContainsKey(lootId))
            {
                loots.Add(lootId, quantity);
            }
            else
            {
                loots[lootId] += quantity;
            }
            OnWarehouseContentChanged?.Invoke(lootId, loots[lootId]);
        }
    }
}
