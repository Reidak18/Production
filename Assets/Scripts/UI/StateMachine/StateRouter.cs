using System.Collections;
using System.Collections.Generic;
using Production.GameLogic;
using UnityEngine;

namespace Production.UI
{
    public class StateRouter : MonoBehaviour
    {
        [SerializeField]
        private UIStateSwitcherManager stateSwitcher;
        [SerializeField]
        private GameSession gameSession;

        private void Start()
        {
            SwitchToMainMenuState();
            gameSession.OnBuildingClicked += OnBuildingClicked;

            var gameState = stateSwitcher.GetState<GameState>();
            List<Loot> loots = new List<Loot>(gameSession.GetLootDescriptions().resourcesList);
            loots.AddRange(gameSession.GetLootDescriptions().productsList);
            gameState.InitLootUIs(loots);
            gameSession.OnWarehouseContentChanged += gameState.UpdateWarehouseContent;
            gameSession.OnWarehouseContentLoaded += gameState.ReloadWarehouseContent;
            gameSession.OnCoinsCountChanged += gameState.UpdateCoinsCount;
        }

        public void SwitchToMainMenuState()
        {
            var mainMenuState = stateSwitcher.GetState<MainMenuState>();
            mainMenuState.Init(RestartGame);
            stateSwitcher.SwitchState(mainMenuState);
        }

        public void RestartGame(int resourceBuildCount)
        {
            gameSession.StartGame(resourceBuildCount);
            SwitchToGameState();
        }

        public void SwitchToGameState()
        {
            var gameState = stateSwitcher.GetState<GameState>();
            stateSwitcher.SwitchState(gameState);
        }

        private void OnBuildingClicked(Building building)
        {
            switch(building.type)
            {
                case BuildingType.Resource:
                    ShowResourceBuilding(building as ResourceBuilding);
                    break;
                case BuildingType.Processing:
                    ShowProcessingBuilding(building as ProcessingBuilding);
                    break;
                case BuildingType.Marketplace:
                    ShowMarketplaceBuilding();
                    break;
                default:
                    break;
            }
        }

        private void ShowResourceBuilding(ResourceBuilding building)
        {
            var resourceState = stateSwitcher.GetState<ResourceState>();
            resourceState.Init(building, gameSession.GetLootDescriptions().resourcesList, SwitchToGameState);
            stateSwitcher.SwitchState(resourceState);
        }

        private void ShowProcessingBuilding(ProcessingBuilding building)
        {
            var processingState = stateSwitcher.GetState<ProcessingState>();
            LootDescriptions lootDescriptions = gameSession.GetLootDescriptions();
            processingState.Init(building, lootDescriptions.resourcesList, lootDescriptions.productsList, gameSession.GetConvertations(), SwitchToGameState);
            stateSwitcher.SwitchState(processingState);
        }

        private void ShowMarketplaceBuilding()
        {
            var marketplaceState = stateSwitcher.GetState<MarketplaceState>();
            marketplaceState.Init(gameSession.GetLootDescriptions().productsList, gameSession.GetWarehouseQuantity, gameSession.SellProduct, SwitchToGameState);
            stateSwitcher.SwitchState(marketplaceState);
        }
    }
}