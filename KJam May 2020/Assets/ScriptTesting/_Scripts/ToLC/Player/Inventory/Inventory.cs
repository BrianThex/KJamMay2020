using System.Collections;
using System.Collections.Generic;
using ToLC.Interactables.Items;
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

        public delegate void OnItemChanged();
        public OnItemChanged onItemChangedCallback;

        public int maxSpace = 20;

		public List<Item> items = new List<Item>();
        public List<Card> cards = new List<Card>();

        public bool Add (Item item)
        {
            if (!item.isDefaultItem)
            {
                if (items.Count >= maxSpace)
                {
                    Debug.Log("Not enough inventory space!");
                    return false;
                }
                items.Add(item);

                if (onItemChangedCallback != null)
                    onItemChangedCallback.Invoke();
            }
            return true;
        }
        public void Remove (Item item)
        {
            items.Remove(item);
            if (onItemChangedCallback != null)
                onItemChangedCallback.Invoke();
        }

        public bool Add(Card card)
        {
            if (!card.isDefaultItem)
            {
                if (cards.Count >= maxSpace)
                {
                    Debug.Log("Not enough inventory space!");
                    return false;
                }
                cards.Add(card);

                if (onItemChangedCallback != null)
                    onItemChangedCallback.Invoke();
            }
            return true;
        }
        public void Remove(Card card)
        {
            cards.Remove(card);
            if (onItemChangedCallback != null)
                onItemChangedCallback.Invoke();
        }
    }
}

