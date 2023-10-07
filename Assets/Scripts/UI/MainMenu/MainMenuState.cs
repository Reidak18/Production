using System;
using UnityEngine;

namespace Production.UI
{
    public class MainMenuState : State
    {
        [SerializeField]
        private MainMenuStateView view;

        private Action<int> onStartGameCallback;

        private void Awake()
        {
            view.OnStartClicked += OnStartClicked;
        }

        public void Init(Action<int> onStartGameCallback)
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
