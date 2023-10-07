using System;
using System.Collections.Generic;
using Production.GameLogic;
using UnityEngine;

namespace Production.UI
{
    public class ResourceState : State
    {
        [SerializeField]
        private ResourceStateView view;

        private ResourceBuilding building;
        private List<Resource> resourcesList;
        private int resourceIndex;

        private Action OnClose;

        private void Awake()
        {
            view.OnChangeClicked += OnChangeClicked;
            view.OnStartClicked += OnStartClicked;
            view.OnCloseClicked += OnCloseClicked;
        }

        public void Init(ResourceBuilding building, List<Resource> resourcesList, Action OnClose)
        {
            this.building = building;
            this.resourcesList = resourcesList;
            this.OnClose = OnClose;

            view.SetProductTime(building.description.productionTime);
            if (building.isWorking)
            {
                resourceIndex = resourcesList.IndexOf(building.resource);
                view.SetResourceImage(building.resource.texture);
            }
            else
            {
                resourceIndex = -1;
                view.SetResourceImage(null);
            }
            view.SetStartButtonEnable(building.isWorking);
            view.UpdateStartButton(building.isWorking);
        }

        private void OnChangeClicked()
        {
            resourceIndex = (resourceIndex + 1) % resourcesList.Count;
            view.SetResourceImage(resourcesList[resourceIndex].texture);
            view.SetStartButtonEnable(true);

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
            building.StartWorking(resourcesList[resourceIndex]);
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
