using System;
using System.Collections.Generic;
using UnityEngine;

namespace Production.GameLogic
{
    [Serializable]
    public class Convertation
    {
        public string[] resourceIds;
        public string productId;
    }

    [CreateAssetMenu(fileName = "ConvertationRules", menuName = "Production/Convertation rules")]
    public class Convertations : ScriptableObject
    {
        public List<Convertation> convertationList;

        public string GetConvertationResult(string[] resourceIds)
        {
            foreach(var convertation in convertationList)
            {
                if (new HashSet<string>(convertation.resourceIds).SetEquals(resourceIds))
                {
                    return convertation.productId;
                }
            }

            return null;
        }
    }
}
