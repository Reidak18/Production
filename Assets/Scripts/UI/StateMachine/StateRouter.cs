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
                    Debug.Log("Show processing");
                    break;
                case BuildingType.Marketplace:
                    Debug.Log("Show marketplace");
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
    }
}