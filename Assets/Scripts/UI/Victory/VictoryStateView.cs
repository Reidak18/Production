using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Production.UI
{
    public class VictoryStateView : MonoBehaviour
    {
        [SerializeField]
        private string descriptionFormat = "Вы заработали {0} монет и прошли игру";

        [Space]

        [SerializeField]
        private Text descriptionText;
        [SerializeField]
        private Button MenuButton;

        public event Action OnMenuClicked;

        private void Awake()
        {
            MenuButton.onClick.AddListener(OnMenuClicked.Invoke);
        }

        public void Init(int targetCoinsCount)
        {
            descriptionText.text = string.Format(descriptionFormat, targetCoinsCount);
        }
    }
}
