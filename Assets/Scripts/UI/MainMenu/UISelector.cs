using UnityEngine;

namespace Production.UI
{
    public class UISelector : MonoBehaviour
    {
        public int selectedValue = -1;
        private SelectorButton[] variantButtons;

        private void Awake()
        {
            variantButtons = GetComponentsInChildren<SelectorButton>();
            if (variantButtons.Length == 0)
            {
                Debug.LogError("No variants button in selector");
                return;
            }

            foreach(var variantButton in variantButtons)
            {
                variantButton.OnSelectedValueChanged += OnSelectedValueChanged;
            }

            selectedValue = variantButtons[0].associatedValue;
            variantButtons[0].SetHighlight(true);
        }

        private void OnSelectedValueChanged(int selectedValue)
        {
            this.selectedValue = selectedValue;
            foreach(var variantButton in variantButtons)
            {
                variantButton.SetHighlight(variantButton.associatedValue == selectedValue);
            }
        }
    }
}
