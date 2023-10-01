using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Production.GameLogic
{
    public class ProductionController : MonoBehaviour
    {
        private bool gameIsStarted = false;
        private float gameTime = 0;
        // ресурсные и перерабатывающие постройки раздельно, чтобы ресурсные имели больший приоритет при добыче секунда в секунду
        private Dictionary<ResourceBuilding, float> resourceBuildings;
        private Dictionary<ProcessingBuilding, float> processingBuildings;

        /// забирает необходимые ресурсы со склада;
        /// принимает словарь, где ключ - ресурс, значение - количество
        /// возвращает true, если на складе хватает ресурсов, иначе false
        private Func<Dictionary<string, int>, bool> getFromProvider;

        /// отправляет выходные ресурсы / продукты на склад
        /// принимает словарь, где ключ - ресурс, значение - количество
        private Action<string, int> sendToConsumer;

        public void StartGame(Func<Dictionary<string, int>, bool> getFromProvider, Action<string, int> sendToConsumer)
        {
            resourceBuildings = new Dictionary<ResourceBuilding, float>();
            processingBuildings = new Dictionary<ProcessingBuilding, float>();
            gameTime = 0;

            this.getFromProvider = getFromProvider;
            this.sendToConsumer = sendToConsumer;

            var buildList = GetComponentsInChildren<Building>();
            foreach(var build in buildList)
            {
                switch(build.type)
                {
                    case BuildingType.Resource:
                        resourceBuildings.Add(build as ResourceBuilding, -1);
                        break;
                    case BuildingType.Processing:
                        processingBuildings.Add(build as ProcessingBuilding, -1);
                        break;
                    default:
                        break;
                }
            }

            gameIsStarted = true;
        }

        public void StopGame()
        {
            gameIsStarted = false;
        }

        private void FixedUpdate()
        {
            if (!gameIsStarted)
                return;

            gameTime += Time.fixedDeltaTime;

            foreach(var build in resourceBuildings.Keys.ToList())
            {
                if (!build.isWorking)
                    continue;

                if (resourceBuildings[build] < 0)
                {
                    resourceBuildings[build] = gameTime;
                    continue;
                }

                if (gameTime - resourceBuildings[build] >= build.description.productionTime)
                {
                    resourceBuildings[build] = gameTime;
                    sendToConsumer.Invoke(build.resource.id, 1);
                }
            }

            foreach (var build in processingBuildings.Keys.ToList())
            {
                if (!build.isWorking)
                    continue;

                Dictionary<string, int> requiredResources = new Dictionary<string, int>()
                {
                    { build.resource1.id, 1 },
                    { build.resource2.id, 2 }
                };

                if (processingBuildings[build] < 0)
                {
                    if (getFromProvider.Invoke(requiredResources))
                        processingBuildings[build] = gameTime;
                    else
                        build.isWorking = false;
                    continue;
                }

                if (gameTime - processingBuildings[build] >= build.description.productionTime)
                {
                    sendToConsumer.Invoke(build.product.id, 1);
                    if (getFromProvider.Invoke(requiredResources))
                        processingBuildings[build] = gameTime;
                    else
                        build.isWorking = false;
                }
            }
        }
    }
}
