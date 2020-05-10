using UnityEngine;

namespace ToLC.Interactables.Items
{
    [CreateAssetMenu(fileName = "New Item", menuName = "Inventory/Item")]
    public class Item : ScriptableObject
    {
        new public string name = "New Item";
        public GameObject prefab = null;
        public Sprite icon = null;
        public bool isDefaultItem = false;
        public bool isStackable = false;
    }
}

