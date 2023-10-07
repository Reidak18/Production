using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace Production.GameLogic
{
    [CreateAssetMenu(fileName = "BuildingType", menuName = "Tiles/BuildingTile")]
    public class BuildingTile : Tile
    {
        public BuildingType buildingType;
    }
}
