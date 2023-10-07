using System;
using System.Collections.Generic;
using UnityEngine;

namespace Production.GameLogic
{
    [Serializable]
    public class Loot
    {
        public string id;
        public Texture2D texture;
    }

    [Serializable]
    public class Resource: Loot
    {

    }

    [Serializable]
    public class Product: Loot
    {
        public int price;
    }

    [CreateAssetMenu(fileName = "LootDescriptions", menuName = "Production/Loot descriptions")]
    public class LootDescriptions : ScriptableObject
    {
        public List<Resource> resourcesList;
        public List<Product> productsList;
    }
}
