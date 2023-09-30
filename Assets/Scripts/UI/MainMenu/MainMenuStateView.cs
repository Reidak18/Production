using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Production.UI
{
    public class MainMenuStateView : MonoBehaviour
    {
        [SerializeField]
        private UISelector resourceBuildCountSelector;
        [SerializeField]
        private Button startButton;

        public event Action OnStartClicked;

        private void Awake()
        {
            startButton.onClick.AddListener(OnStartClicked.Invoke);
        }

        public int GetResourceBuildCount()
        {
            return resourceBuildCountSelector.selectedValue;
        }
    }
}
