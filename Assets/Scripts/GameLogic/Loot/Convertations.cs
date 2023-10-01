using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Production.GameLogic
{
    [Serializable]
    public class Convertation
    {
        public string[] resourceIds;
        public string productId;
    }

    [CreateAssetMenu(fileName = "ConvertationRules", menuName = "Convertation rules")]
    public class Convertations : ScriptableObject
    {
        public List<Convertation> convertationList;

        public string GetConvertationResult(string[] resourceIds)
        {
            foreach(var convertation in convertationList)
            {
                if (Enumerable.SequenceEqual(convertation.resourceIds, resourceIds))
                {
                    return convertation.productId;
                }
            }

            return null;
        }
    }
}
