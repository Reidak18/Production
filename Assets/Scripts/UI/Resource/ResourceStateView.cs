using System;
using UnityEngine;
using UnityEngine.UI;

namespace Production.UI
{
    public class ResourceStateView : MonoBehaviour
    {
        [SerializeField]
        private string startText = "Старт";
        [SerializeField]
        private string productTimeFormat = "Выработка единицы ресурса: {0} c";
        [SerializeField]
        private string stopText = "Стоп";

        [Space]

        [SerializeField]
        private Text productTimeText;
        [SerializeField]
        private Image resourceImage;
        [SerializeField]
        private Button changeButton;
        [SerializeField]
        private Button startButton;
        [SerializeField]
        private Text startButtonText;
        [SerializeField]
        private Button closeButton;

        public event Action OnChangeClicked;
        public event Action OnStartClicked;
        public event Action OnCloseClicked;

        private void Awake()
        {
            changeButton.onClick.AddListener(OnChangeClicked.Invoke);
            startButton.onClick.AddListener(OnStartClicked.Invoke);
            closeButton.onClick.AddListener(OnCloseClicked.Invoke);
        }

        public void SetProductTime(float productTime)
        {
            productTimeText.text = string.Format(productTimeFormat, productTime);
        }

        public void SetResourceImage(Texture2D resourceTexture)
        {
            if (resourceTexture == null)
            {
                resourceImage.sprite = null;
                Color imageColor = resourceImage.color;
                imageColor.a = 0;
                resourceImage.color = imageColor;
            }
            else
            {
                resourceImage.sprite = Sprite.Create(resourceTexture, new Rect(0.0f, 0.0f, resourceTexture.width, resourceTexture.height), new Vector2(0.5f, 0.5f), 100.0f);
                Color imageColor = resourceImage.color;
                imageColor.a = 1;
                resourceImage.color = imageColor;
            }
        }

        public void UpdateStartButton(bool isWorking)
        {
            startButtonText.text = isWorking ? stopText : startText;
        }

        public void SetStartButtonEnable(bool isEnable)
        {
            startButton.interactable = isEnable;
        }
    }
}
