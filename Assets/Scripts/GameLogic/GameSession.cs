using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace Production.GameLogic
{
    public class GameSession : MonoBehaviour
    {
        private const string coinsCountKey = "CoinsCount";

        [SerializeField]
        private Grid tilemap;
        [SerializeField]
        private BuildingFactory buildingFactory;
        [SerializeField]
        private LootDescriptions lootDescriptions;

        private MapController currentMap;
        private Warehouse warehouse;
        private int coinsCount = 0;

        private Dictionary<Vector3Int, Building> buildings;

        public event Action<BuildingType> OnBuildingClicked;

        private void Awake()
        {
            warehouse = new Warehouse();
        }

        public void StartGame(int resourceBuildCount)
        {
            Destroy(currentMap?.gameObject);
            buildings = CreateMap(resourceBuildCount);
            warehouse.Load(lootDescriptions);
            LoadCoins();
        }

        private void OnApplicationFocus(bool focus)
        {
            if (!focus)
            {
                warehouse.Save();
                SaveCoins();
            }
        }

        private Dictionary<Vector3Int, Building> CreateMap(int resourceBuildCount)
        {
            currentMap = Instantiate(tilemap).GetComponent<MapController>();
            currentMap.OnBuildingClicked += OnBuildingClickedHandler;
            return currentMap.CreateBuildingDictionary(resourceBuildCount, buildingFactory);
        }

        private void OnBuildingClickedHandler(Vector3Int clickCellPos)
        {
            if (buildings.TryGetValue(clickCellPos, out Building clickedBuilding))
            {
                OnBuildingClicked?.Invoke(clickedBuilding.type);
            }
        }

        private void LoadCoins()
        {
            coinsCount = PlayerPrefs.GetInt(coinsCountKey, 0);
        }

        private void SaveCoins()
        {
            PlayerPrefs.SetInt(coinsCountKey, coinsCount);
        }
    }
}
