using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Production.UI
{
    public class VictoryState : State
    {
        [SerializeField]
        private VictoryStateView view;

        private Action OnBackToMenu;

        private void Awake()
        {
            view.OnMenuClicked += OnMenuClicked;
        }

        public void Init(int targetCoinsCount, Action OnBackToMenu)
        {
            view.Init(targetCoinsCount);
            this.OnBackToMenu = OnBackToMenu;
        }

        private void OnMenuClicked()
        {
            OnBackToMenu?.Invoke();
        }
    }
}
