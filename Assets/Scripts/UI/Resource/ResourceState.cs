using System;
using System.Collections;
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
            view.SetStartButtonEnable(resourceIndex != -1);
            view.UpdateStartButton(building.isWorking);
        }

        private void OnChangeClicked()
        {
            resourceIndex = (resourceIndex + 1) % resourcesList.Count;
            view.SetResourceImage(resourcesList[resourceIndex].texture);
            view.SetStartButtonEnable(true);
        }

        private void OnStartClicked()
        {
            building.isWorking = !building.isWorking;
            view.UpdateStartButton(building.isWorking);
            building.resource = building.isWorking ? resourcesList[resourceIndex] : null;
        }

        private void OnCloseClicked()
        {
            OnClose?.Invoke();
        }
    }
}
