using System;
using System.Collections;
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
        private int[] resourceIndexes = new int[2];

        private Action OnClose;

        private void Awake()
        {
            view.OnChangeClicked += OnChangeClicked;
            view.OnStartClicked += OnStartClicked;
            view.OnCloseClicked += OnCloseClicked;
        }

        public void Init(ProcessingBuilding building, List<Resource> resourcesList, List<Product> productsList, Convertations convertations, Action OnClose)
        {
            this.building = building;
            this.resourcesList = resourcesList;
            this.productsList = productsList;
            this.convertations = convertations;
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
        }

        private void OnChangeClicked(int index)
        {
            resourceIndexes[index] = (resourceIndexes[index] + 1) % resourcesList.Count;
            view.SetResourceImage(index, resourcesList[resourceIndexes[index]].texture);

            List<string> resourceList = new List<string>();
            for (int i = 0; i < resourceIndexes.Length; i++)
            {
                if (resourceIndexes[i] != -1)
                {
                    resourceList.Add(resourcesList[resourceIndexes[i]].id);
                }
            }
            string convertationResultId = convertations.GetConvertationResult(resourceList.ToArray());
            if (!string.IsNullOrEmpty(convertationResultId))
            {
                view.SetProductImage(productsList.FirstOrDefault(p => p.id == convertationResultId).texture);
                view.SetStartButtonEnable(true);
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
            List<Resource> resourceList = new List<Resource>();
            for (int i = 0; i < resourceIndexes.Length; i++)
            {
                resourceList.Add(resourcesList[resourceIndexes[i]]);
            }
            string targetProductId = convertations.GetConvertationResult(resourceList.Select(r => r.id).ToArray());
            if (string.IsNullOrEmpty(targetProductId))
            {
                Debug.LogError("Can't get result product from given resources");
                return;
            }
            Product targetProduct = productsList.FirstOrDefault(p => p.id == targetProductId);
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
    }
}
