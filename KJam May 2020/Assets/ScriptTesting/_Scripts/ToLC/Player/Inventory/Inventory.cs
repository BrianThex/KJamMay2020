using Photon.Pun;
using System.Collections.Generic;
using ToLC.Interactables.Items;
using UnityEngine;

namespace ToLC.Player.Inventory
{
    public class Inventory : MonoBehaviourPun
    {
        #region Singleton
        public static Inventory instance;
        public InventoryUI iUI = null;
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

        public delegate void onItemChanged();
        public onItemChanged onItemChangedCallback;

        public delegate void onCardChanged();
        public onCardChanged onCardChangedCallback;

        public int maxSpace = 20;

		public List<Item> items = new List<Item>();
        public List<Card> cards = new List<Card>();

        //public bool Add (Item item)
        //{
        //    if (!item.isDefaultItem)
        //    {
        //        if (items.Count >= maxSpace)
        //        {
        //            Debug.Log("Not enough inventory space!");
        //            return false;
        //        }
        //        items.Add(item);

        //        if (onItemChangedCallback != null)
        //            onItemChangedCallback.Invoke();
        //    }
        //    return true;
        //}
        //public void Remove (Item item)
        //{
        //    items.Remove(item);
        //    if (onItemChangedCallback != null)
        //        onItemChangedCallback.Invoke();
        //}
        
        public void SendInventoryItems()
        {
            photonView.RPC("GETInventoryItems", RpcTarget.Others, items as object, cards as object);
        }

        [PunRPC]
        public void GETInventoryItems(List<Item>newItems, List<Card> newCards)
        {
            items.Clear();
            foreach (Item i in newItems)
            {
                items.Add(i);
            }
            cards.Clear();
            foreach (Card c in newCards)
            {
                cards.Add(c);
            }
        }

        [PunRPC]
        public bool Add(Card card)
        {
            Debug.Log(card);
            if (!card.isDefaultItem)
            {
                if (cards.Count >= maxSpace)
                {
                    Debug.Log("Not enough inventory space!");
                    return false;
                }
                cards.Add(card);

                if (onCardChangedCallback != null)
                {
                    onCardChangedCallback.Invoke();
                }
            }
            return true;
        }
        public void Remove(Card card)
        {
            cards.Remove(card);
            if (onCardChangedCallback != null)
            {
                onCardChangedCallback.Invoke();
            }
        }
    }
}

