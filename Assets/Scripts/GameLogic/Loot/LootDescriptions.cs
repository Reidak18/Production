using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Production.GameLogic
{
    [Serializable]
    public class Loot
    {
        public string id;
        public string readableName;
        public Texture texture;
    }

    [Serializable]
    public class Resource: Loot
    {

    }

    [Serializable]
    public class Product: Loot
    {
        public int cost;
    }

    [CreateAssetMenu(fileName = "LootDescriptions", menuName = "Loot descriptions")]
    public class LootDescriptions : ScriptableObject
    {
        [SerializeField]
        public List<Resource> resourcesList;
        [SerializeField]
        public List<Product> productsList;
    }
}