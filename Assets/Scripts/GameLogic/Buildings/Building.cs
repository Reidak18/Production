using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Production.GameLogic
{
    public enum BuildingType { Resource, Processing, Marketplace };

    public abstract class Building : MonoBehaviour
    {
        public BuildingType type;
        protected BuildingDescription description;

        /// <summary>
        /// запуск рабочего процесса
        /// </summary>
        /// <param name="getFromProvider"></param>
        /// забирает необходимые ресурсы со склада;
        /// принимает словарь, где ключ - ресурс, значение - количество
        /// возвращает true, если на складе хватает ресурсов, иначе false
        /// <param name="sendToConsumer"></param>
        /// отправляет выходные ресурсы / продукты на склад
        /// принимает словарь, где ключ - ресурс, значение - количество
        public abstract void StartWorking(Func<Dictionary<Loot, int>, bool> getFromProvider, Action<Dictionary<Loot, int>> sendToConsumer);

        public virtual void StopWorking()
        {
            StopAllCoroutines();
        }
    }
}