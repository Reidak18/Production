using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Production.UI
{
    // Стейт главного меню игры
    public class MainMenu : MonoBehaviour
    {
        [SerializeField]
        private MainMenuView view;

        private void Awake()
        {
            view.OnStartClicked.AddListener(OnStartClicked);
        }

        private void OnStartClicked()
        {
            int resourceBuildCount = view.GetResourceBuildCount();
            if (resourceBuildCount <= 0)
            {
                Debug.LogError("Can't create game with equal or less 0 resource buildings");
                return;
            }

            Debug.Log(view.GetResourceBuildCount());
        }
    }
}
