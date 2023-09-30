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
        }

        public void SwitchToMainMenuState()
        {
            var mainMenuState = stateSwitcher.GetState<MainMenuState>();
            mainMenuState.Init(SwitchToGameState);
            stateSwitcher.SwitchState(mainMenuState);
        }

        public void SwitchToGameState(int resourceBuildCount)
        {
            gameSession.StartGame(resourceBuildCount);
            var gameState = stateSwitcher.GetState<GameState>();
            gameState.Init();
            stateSwitcher.SwitchState(gameState);
        }

        private void OnBuildingClicked(BuildingType type)
        {
            switch(type)
            {
                case BuildingType.Resource:
                    Debug.Log("Show resource");
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
    }
}