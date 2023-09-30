using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Production.UI
{
    [RequireComponent(typeof(Button))]
    public class SelectorButton : MonoBehaviour
    {
        public int associatedValue;
        private Button associatedButton;
        private Button AssociatedButton
        {
            get
            {
                if (associatedButton == null)
                    associatedButton = GetComponent<Button>();
                return associatedButton;
            }
        }

        public event Action<int> OnSelectedValueChanged;

        private void Awake()
        {
            AssociatedButton.onClick.AddListener(OnButtonSelected);
        }

        private void OnButtonSelected()
        {
            OnSelectedValueChanged.Invoke(associatedValue);
        }

        // выбранная кнопка выделяется красным цветом; невыбранные кнопки белые
        public void SetHighlight(bool isHighlight)
        {
            var colors = AssociatedButton.colors;

            if (isHighlight)
            {
                colors.normalColor = Color.red;
                colors.selectedColor = Color.red;
            }
            else
            {
                colors.normalColor = Color.white;
                colors.selectedColor = Color.white;
            }

            AssociatedButton.colors = colors;
        }
    }
}
