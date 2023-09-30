using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Production.GameLogic
{
    public class Warehouse
    {
        public Dictionary<Resource, int> resources = new Dictionary<Resource, int>();
        public Dictionary<Product, int> products = new Dictionary<Product, int>();

        public void Load(LootDescriptions lootDescriptions)
        {
            resources.Clear();
            products.Clear();

            foreach (var resource in lootDescriptions.resourcesList)
            {
                resources.Add(resource, PlayerPrefs.GetInt(resource.id, 0));
            }
            foreach (var product in lootDescriptions.productsList)
            {
                products.Add(product, PlayerPrefs.GetInt(product.id, 0));
            }
        }

        public void Save()
        {
            foreach (var resource in resources.Keys)
            {
                PlayerPrefs.SetInt(resource.id, resources[resource]);
            }
            foreach (var product in products.Keys)
            {
                PlayerPrefs.SetInt(product.id, products[product]);
            }
        }
    }
}
