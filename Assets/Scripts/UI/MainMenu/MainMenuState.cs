using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Production.UI
{
    // Стейт главного меню игры
    public class MainMenuState : State
    {
        [SerializeField]
        private MainMenuStateView view;

        private UnityAction<int> onStartGameCallback;

        private void Awake()
        {
            view.OnStartClicked.AddListener(OnStartClicked);
        }

        public void Init(UnityAction<int> onStartGameCallback)
        {
            this.onStartGameCallback = onStartGameCallback;
        }

        private void OnStartClicked()
        {
            int resourceBuildCount = view.GetResourceBuildCount();
            if (resourceBuildCount <= 0)
            {
                Debug.LogError("Can't create game with equal or less 0 resource buildings");
                return;
            }

            onStartGameCallback?.Invoke(view.GetResourceBuildCount());
        }
    }
}
