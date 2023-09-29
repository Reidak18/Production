using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Production.UI
{
    public class StateRouter : MonoBehaviour
    {
        [SerializeField]
        private UIStateSwitcherManager stateSwitcher;

        private void Start()
        {
            SwitchToMainMenuState();
        }

        public void SwitchToMainMenuState()
        {
            var mainMenuState = stateSwitcher.GetState<MainMenuState>();
            mainMenuState.Init(SwitchToGameState);
            stateSwitcher.SwitchState(mainMenuState);
        }

        public void SwitchToGameState(int resourceBuildsCount)
        {
            Debug.Log(resourceBuildsCount);
            var gameState = stateSwitcher.GetState<GameState>();
            gameState.Init();
            stateSwitcher.SwitchState(gameState);
        }
    }
}