using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Production.GameLogic
{
    public enum BuildingType { Resource, Processing, Marketplace };

    public abstract class Building : MonoBehaviour
    {
        // координаты в роли уникального идентификатора
        public Vector3Int coords;
        public BuildingType type;
        public BuildingDescription description;

        public abstract void StopWorking();

        public virtual void Init(BuildingDescription description, Vector3Int coords)
        {
            this.description = description;
            this.coords = coords;
        }
    }
}