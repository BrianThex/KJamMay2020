using System.Collections;
using System.Collections.Generic;
using ToLC.Items;
using UnityEngine;

namespace ToLC.Player.Inventory
{
    public class Inventory : MonoBehaviour
    {
		#region Singleton
		public static Inventory instance;
        private void Awake()
        {
            if (instance != null)
            {
                Debug.LogWarning("more than on instance of Inventory found");
                return;
            }
            instance = this;
        }
		#endregion

		public List<Item> items = new List<Item>();

        public void Add (Item item)
        {
            if (!item.isDefaultItem)
                items.Add(item);
        }
        public void Remove (Item item)
        {
            items.Remove(item);
        }
    }
}

