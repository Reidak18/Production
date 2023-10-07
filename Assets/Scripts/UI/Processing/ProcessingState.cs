using System;
using System.Collections.Generic;
using System.Linq;
using Production.GameLogic;
using UnityEngine;

namespace Production.UI
{
    public class ProcessingState : State
    {
        [SerializeField]
        private ProcessingStateView view;

        private ProcessingBuilding building;
        private List<Resource> resourcesList;
        private List<Product> productsList;
        private Convertations convertations;
        private Func<string, int> GetAvailableQuantity;
        private int[] resourceIndexes = new int[2];

        private Action OnClose;

        private void Awake()
        {
            view.OnChangeClicked += OnChangeClicked;
            view.OnStartClicked += OnStartClicked;
            view.OnCloseClicked += OnCloseClicked;
        }

        public void Init(ProcessingBuilding building, List<Resource> resourcesList, List<Product> productsList,
            Convertations convertations, Func<string, int> GetAvailableQuantity, Action OnClose)
        {
            this.building = building;
            this.resourcesList = resourcesList;
            this.productsList = productsList;
            this.convertations = convertations;
            this.GetAvailableQuantity = GetAvailableQuantity;
            this.OnClose = OnClose;

            view.SetProductTime(building.description.productionTime);
            if (building.isWorking)
            {
                for (int i = 0; i < resourceIndexes.Length; i++)
                {
                    resourceIndexes[i] = resourcesList.IndexOf(building.resources[i]);
                    view.SetResourceImage(i, building.resources[i].texture);
                }
                view.SetProductImage(building.product.texture);
            }
            else
            {
                for (int i = 0; i < resourceIndexes.Length; i++)
                {
                    resourceIndexes[i] = -1;
                    view.SetResourceImage(i, null);
                }
                view.SetProductImage(null);
            }
            view.SetStartButtonEnable(building.isWorking);
            view.UpdateStartButton(building.isWorking);

            building.OnWorkingStopped += OnWorkingStopped;
        }

        // пользователь может просматривать комбинации различных ресурсов; в случае когда из них можно получить
        // готовый продукт он отобразится в окне результата
        // кнопка запуска доступна только если комбинация ресурсов дает готовый продукт и требуемые ресурсы есть на складе
        private void OnChangeClicked(int index)
        {
            resourceIndexes[index] = (resourceIndexes[index] + 1) % resourcesList.Count;
            view.SetResourceImage(index, resourcesList[resourceIndexes[index]].texture);

            string convertationResultId = GetConvertationResult();
            if (!string.IsNullOrEmpty(convertationResultId))
            {
                view.SetProductImage(productsList.FirstOrDefault(p => p.id == convertationResultId).texture);
                view.SetStartButtonEnable(CheckResourceEnough());
            }
            else
            {
                view.SetProductImage(null);
                view.SetStartButtonEnable(false);
            }

            StopWorking();
        }

        private void OnStartClicked()
        {
            if (!building.isWorking)
                StartWorking();
            else
                StopWorking();
        }

        private void StartWorking()
        {
            string targetProductId = GetConvertationResult();
            if (string.IsNullOrEmpty(targetProductId))
            {
                Debug.LogError("Can't get result product from given resources");
                return;
            }
            Product targetProduct = productsList.FirstOrDefault(p => p.id == targetProductId);
            List<Resource> resourceList = new List<Resource>();
            for (int i = 0; i < resourceIndexes.Length; i++)
            {
                resourceList.Add(resourcesList[resourceIndexes[i]]);
            }
            building.StartWorking(resourceList.ToArray(), targetProduct);
            view.UpdateStartButton(building.isWorking);
        }

        private void StopWorking()
        {
            building.StopWorking();
            view.UpdateStartButton(building.isWorking);
        }

        private void OnCloseClicked()
        {
            OnClose?.Invoke();
        }

        private string GetConvertationResult()
        {
            List<string> resourceList = new List<string>();
            for (int i = 0; i < resourceIndexes.Length; i++)
            {
                if (resourceIndexes[i] != -1)
                {
                    resourceList.Add(resourcesList[resourceIndexes[i]].id);
                }
            }
            return convertations.GetConvertationResult(resourceList.ToArray());
        }

        private bool CheckResourceEnough()
        {
            foreach (var resourceIndex in resourceIndexes)
            {
                if (resourceIndex == -1 || GetAvailableQuantity(resourcesList[resourceIndex].id) <= 0)
                {
                    return false;
                }
            }
            return true;
        }

        private void OnWorkingStopped()
        {
            view.UpdateStartButton(false);
            view.SetStartButtonEnable(!string.IsNullOrEmpty(GetConvertationResult()) && CheckResourceEnough());
        }

        public override void OnStatePreHidden()
        {
            base.OnStatePreHidden();
            building.OnWorkingStopped -= OnWorkingStopped;
        }
    }
}
