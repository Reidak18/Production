using System;
using UnityEngine;
using UnityEngine.UI;

namespace Production.UI
{
    public class ProcessingStateView : MonoBehaviour
    {
        [SerializeField]
        private string startText = "Старт";
        [SerializeField]
        private string productTimeFormat = "Выработка единицы продукта: {0} c";
        [SerializeField]
        private string stopText = "Стоп";

        [Space]

        [SerializeField]
        private Text productTimeText;
        [SerializeField]
        private Image[] resourceImage;
        [SerializeField]
        private Image productImage;
        [SerializeField]
        private Button[] changeButtons;
        [SerializeField]
        private Button startButton;
        [SerializeField]
        private Text startButtonText;
        [SerializeField]
        private Button closeButton;

        public event Action<int> OnChangeClicked;
        public event Action OnStartClicked;
        public event Action OnCloseClicked;

        private void Awake()
        {
            for (int i = 0; i < changeButtons.Length; i++)
            {
                int index = i;
                changeButtons[index].onClick.AddListener(() => OnChangeClicked.Invoke(index));
            }
            startButton.onClick.AddListener(OnStartClicked.Invoke);
            closeButton.onClick.AddListener(OnCloseClicked.Invoke);
        }

        public void SetProductTime(float productTime)
        {
            productTimeText.text = string.Format(productTimeFormat, productTime);
        }

        public void SetResourceImage(int index, Texture2D resourceTexture)
        {
            SetImage(resourceImage[index], resourceTexture);
        }

        public void SetProductImage(Texture2D resourceTexture)
        {
            SetImage(productImage, resourceTexture);
        }

        private void SetImage(Image lootImage, Texture2D resourceTexture)
        {
            if (resourceTexture == null)
            {
                lootImage.sprite = null;
                Color imageColor = lootImage.color;
                imageColor.a = 0;
                lootImage.color = imageColor;
            }
            else
            {
                lootImage.sprite = Sprite.Create(resourceTexture, new Rect(0.0f, 0.0f, resourceTexture.width, resourceTexture.height), new Vector2(0.5f, 0.5f), 100.0f);
                Color imageColor = lootImage.color;
                imageColor.a = 1;
                lootImage.color = imageColor;
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
