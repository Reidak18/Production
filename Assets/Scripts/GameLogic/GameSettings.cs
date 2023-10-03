using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Production.GameLogic
{
    [CreateAssetMenu(fileName = "GameSettings", menuName = "Production/Game settings")]
    public class GameSettings : ScriptableObject
    {
        public int coinsToWin = 100;
    }
}
