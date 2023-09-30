using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace Production.GameLogic
{
    public class MapController : MonoBehaviour
    {
        [SerializeField]
        private Tilemap buildingsTilemap;

        public event Action<Vector3Int> OnBuildingClicked;

        public Dictionary<Vector3Int, Building> CreateBuildingDictionary(int resourceBuildCount, BuildingFactory factory)
        {
            Dictionary<Vector3Int, Building> buildings = new Dictionary<Vector3Int, Building>();

            int resourceCurrentCount = 0;
            foreach (var pos in buildingsTilemap.cellBounds.allPositionsWithin)
            {
                Vector3Int localPlace = new Vector3Int(pos.x, pos.y, pos.z);
                if (buildingsTilemap.HasTile(localPlace))
                {
                    TileBase tile = buildingsTilemap.GetTile(localPlace);
                    switch (tile.name)
                    {
                        case "Resource":
                            if (resourceCurrentCount < resourceBuildCount)
                            {
                                buildings.Add(new Vector3Int(pos.x, pos.y, pos.z), factory.CreateResourceBuilding(resourceCurrentCount));
                                resourceCurrentCount += 1;
                            }
                            else
                            {
                                buildingsTilemap.SetTile(new Vector3Int(pos.x, pos.y, pos.z), null);
                            }
                            break;
                        case "Processing":
                            buildings.Add(new Vector3Int(pos.x, pos.y, pos.z), factory.CreateProcessingBuilding(0));
                            break;
                        case "Marketplace":
                            buildings.Add(new Vector3Int(pos.x, pos.y, pos.z), factory.CreateMarketplaceBuilding(0));
                            break;
                        default:
                            break;
                    }
                }
            }

            return buildings;
        }

        private void Update()
        {
            if (Input.GetMouseButtonDown(0))
            {
                Vector3 clickWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                clickWorldPos.z = 0;
                Vector3Int clickCellPos = buildingsTilemap.WorldToCell(clickWorldPos);
                if (buildingsTilemap.HasTile(clickCellPos))
                {
                    OnBuildingClicked?.Invoke(clickCellPos);
                }
            }
        }
    }
}
