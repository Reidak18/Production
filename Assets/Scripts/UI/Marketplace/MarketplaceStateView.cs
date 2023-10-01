using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Production.UI
{
    public class MarketplaceStateView : MonoBehaviour
    {
        [SerializeField]
        private string priceFormat = "Цена: {0} монет";

        [Space]

        [SerializeField]
        private Text priceText;
        [SerializeField]
        private Image productImage;
        [SerializeField]
        private Button changeButton;
        [SerializeField]
        private Button sellButton;
        [SerializeField]
        private Button closeButton;

        public event Action OnChangeClicked;
        public event Action OnSellClicked;
        public event Action OnCloseClicked;

        private void Awake()
        {
            changeButton.onClick.AddListener(OnChangeClicked.Invoke);
            sellButton.onClick.AddListener(OnSellClicked.Invoke);
            closeButton.onClick.AddListener(OnCloseClicked.Invoke);
        }

        public void SetPrice(float price)
        {
            priceText.text = string.Format(priceFormat, price);
        }

        public void SetProductImage(Texture2D resourceTexture)
        {
            if (resourceTexture == null)
            {
                productImage.sprite = null;
                Color imageColor = productImage.color;
                imageColor.a = 0;
                productImage.color = imageColor;
            }
            else
            {
                productImage.sprite = Sprite.Create(resourceTexture, new Rect(0.0f, 0.0f, resourceTexture.width, resourceTexture.height), new Vector2(0.5f, 0.5f), 100.0f);
                Color imageColor = productImage.color;
                imageColor.a = 1;
                productImage.color = imageColor;
            }
        }

        public void SetSellButtonEnable(bool isEnable)
        {
            sellButton.interactable = isEnable;
        }
    }
}
