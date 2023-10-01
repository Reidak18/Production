using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace Production.GameLogic
{
    public class GameSession : MonoBehaviour
    {
        [SerializeField]
        private bool clearSavesOnStart;

        private const string coinsCountKey = "CoinsCount";

        [SerializeField]
        private Grid tilemap;
        [SerializeField]
        private BuildingFactory buildingFactory;
        [SerializeField]
        private ProductionController productionController;
        [SerializeField]
        private LootDescriptions lootDescriptions;
        [SerializeField]
        private Convertations convertationList;

        private MapController currentMap;
        private Warehouse warehouse;
        private int coinsCount = 0;

        private Dictionary<Vector3Int, Building> buildings;

        public event Action<Building> OnBuildingClicked;
        public event Action<string, int> OnWarehouseContentChanged;
        public event Action<Dictionary<string, int>> OnWarehouseContentLoaded;

        private void Awake()
        {
            warehouse = new Warehouse();
            warehouse.OnWarehouseContentChanged += (id, quantity) => OnWarehouseContentChanged?.Invoke(id, quantity);
            warehouse.OnWarehouseContentLoaded += (loots) => OnWarehouseContentLoaded?.Invoke(loots);

            if (clearSavesOnStart)
                PlayerPrefs.DeleteAll();
        }

        public void StartGame(int resourceBuildCount)
        {
            Destroy(currentMap?.gameObject);
            buildings = CreateMap(resourceBuildCount);
            warehouse.Load(lootDescriptions);
            LoadCoins();

            productionController.StartGame(warehouse.GetFromWarehouse, warehouse.AddToWarehouse);
        }

        public LootDescriptions GetLootDescriptions()
        {
            return lootDescriptions;
        }

        public Convertations GetConvertations()
        {
            return convertationList;
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
                OnBuildingClicked?.Invoke(clickedBuilding);
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
