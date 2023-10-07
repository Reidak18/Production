using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Production.GameLogic;

namespace Production.UI
{
    public class GameStateView : MonoBehaviour
    {
        [SerializeField]
        private LootUI lootUIPrefab;
        [SerializeField]
        private Transform resourceParent;
        [SerializeField]
        private Transform productParent;
        [SerializeField]
        private Text CoinsCountText;

        private Dictionary<string, LootUI> lootsDict = new Dictionary<string, LootUI>();

        public void InitLootUIs(List<Loot> loots)
        {
            foreach(var loot in loots)
            {
                LootUI lootUI;
                if (loot is Resource)
                {
                    lootUI = Instantiate(lootUIPrefab, resourceParent);
                }
                else
                {
                    lootUI = Instantiate(lootUIPrefab, productParent);
                }
                lootUI.SetIcon(loot.texture);
                lootsDict.Add(loot.id, lootUI);
            }
        }

        public void UpdateQuantity(string lootId, int quantity)
        {
            lootsDict[lootId].SetQuantity(quantity);
        }

        public void ReloadAllQuantity(Dictionary<string, int> loots)
        {
            foreach(var lootId in lootsDict.Keys)
            {
                if (loots.TryGetValue(lootId, out int quantity))
                    lootsDict[lootId].SetQuantity(quantity);
                else
                    lootsDict[lootId].SetQuantity(0);
            }
        }

        public void UpdateCoinsCount(int coinsCount)
        {
            CoinsCountText.text = coinsCount.ToString();
        }
    }
}
