using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Production.UI
{
    public class LootUI : MonoBehaviour
    {
        [SerializeField]
        private Image resourceIcon;
        [SerializeField]
        private Text resourceQuantity;

        public void SetIcon(Texture2D resourceTexture)
        {
            resourceIcon.sprite = Sprite.Create(resourceTexture, new Rect(0.0f, 0.0f, resourceTexture.width, resourceTexture.height), new Vector2(0.5f, 0.5f), 100.0f);
        }

        public void SetQuantity(int quantity)
        {
            resourceQuantity.text = quantity.ToString();
        }
    }
}