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

        public UnityEvent<int> OnSelectedValueChanged;

        private void Awake()
        {
            associatedButton = GetComponent<Button>();
            associatedButton.onClick.AddListener(OnButtonSelected);
        }

        private void OnButtonSelected()
        {
            OnSelectedValueChanged.Invoke(associatedValue);
        }

        // выбранная кнопка выделяется красным цветом; невыбранные кнопки белые
        public void SetHighlight(bool isHighlight)
        {
            var colors = associatedButton.colors;

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

            associatedButton.colors = colors;
        }
    }
}
