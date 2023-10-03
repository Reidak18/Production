using System;
using System.Collections;
using System.Collections.Generic;
using Production.GameLogic;
using UnityEngine;

namespace Production.UI
{
    public class MarketplaceState : State
    {
        [SerializeField]
        private MarketplaceStateView view;

        private List<Product> productsList;
        private int productIndex;

        Func<string, int> GetAvailableQuantity;
        private Action<string, int> OnSell;
        private Action OnClose;

        private void Awake()
        {
            view.OnChangeClicked += OnChangeClicked;
            view.OnSellClicked += OnCellClicked;
            view.OnCloseClicked += OnCloseClicked;
        }

        public void Init(List<Product> productsList, Func<string, int> GetAvailableQuantity, Action<string, int> OnSell, Action OnClose)
        {
            this.productsList = productsList;
            this.GetAvailableQuantity = GetAvailableQuantity;
            this.OnSell = OnSell;
            this.OnClose = OnClose;

            productIndex = -1;
            view.SetProductImage(null);
            view.SetPrice(0);
            view.SetSellButtonEnable(false);
        }

        // перебираются только те продукты, которые есть на складе
        private void OnChangeClicked()
        {
            productIndex = productIndex == -1 ? 0 : productIndex;
            int productIndexOld = productIndex;
            do
            {
                productIndex = (productIndex + 1) % productsList.Count;
                int quantity = GetAvailableQuantity(productsList[productIndex].id);
                if (quantity > 0)
                {
                    view.SetPrice(productsList[productIndex].price);
                    view.SetProductImage(productsList[productIndex].texture);
                    view.SetSellButtonEnable(true);
                    break;
                }
            } while (productIndex != productIndexOld);
        }

        // продажа по одному
        private void OnCellClicked()
        {
            OnSell?.Invoke(productsList[productIndex].id, 1);
            if (GetAvailableQuantity(productsList[productIndex].id) == 0)
            {
                view.SetPrice(0);
                view.SetProductImage(null);
                view.SetSellButtonEnable(false);
            }
        }

        private void OnCloseClicked()
        {
            OnClose?.Invoke();
        }
    }
}