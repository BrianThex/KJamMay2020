using Photon.Pun;
using ToLC.Interactables.Items;
using UnityEngine;
using UnityEngine.UI;

namespace ToLC.Player.Inventory
{
    public class InventoryItem : MonoBehaviour
    {
        public Transform itemContainer = null;

        public GameObject inventorySlot = null;
        public Image icon = null;

        private Card card;
        private Item item;

        //public void AddItem(Item newItem)
        //{
        //    foreach (Item existing in Inventory.instance.items)
        //    {
        //        if (existing.name == newItem.name)
        //        {
        //            // add to the count
        //        }
        //        else
        //        {
        //            item = newItem;
        //            inventorySlot.SetActive(true);
        //            icon.sprite = item.icon;
        //            icon.enabled = true;
        //        }
        //    }
        //}

        //public void ClearItem()
        //{
        //    item = null;

        //    icon.sprite = null;
        //    icon.enabled = false;

        //    inventorySlot.SetActive(false);
        //}

        public void AddCard(Card newCard)
        {
            foreach (Card existing in Inventory.instance.cards)
            {
                //if (existing.name == newCard.name)
                //{
                //    // add to the count
                //}
                //else
                //{
                //    card = newCard;
                //    inventorySlot.SetActive(true);
                //    icon.sprite = card.icon;
                //    icon.enabled = true;
                //}

                card = newCard;
                inventorySlot.SetActive(true);
                icon.sprite = card.icon;
                icon.enabled = true;
            }
        }

        public void ClearCard()
        {
            card = null;

            icon.sprite = null;
            icon.enabled = false;

            inventorySlot.SetActive(false);
        }
    }
}

