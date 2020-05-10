using UnityEngine;

namespace ToLC.Interactables.Items
{
    [CreateAssetMenu(fileName = "New LootSet", menuName = "Inventory/LootSet")]
    public class LootSet : ScriptableObject
    {
        public Loot Loot;
    }

    [System.Serializable]
    public class Loot
    {
        public Item[] standardLoot;
        public Card[] cardLoot;
    }
}

