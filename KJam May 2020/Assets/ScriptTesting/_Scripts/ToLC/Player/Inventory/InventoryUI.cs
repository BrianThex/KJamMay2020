using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using ToLC.Interactables.Items;
using UnityEngine;

namespace ToLC.Player.Inventory
{
    public class InventoryUI : MonoBehaviourPun
    {
        public Transform itemContainer;

        public InventoryItem[] inventoryItems;

        private void Start()
        {
            //Inventory.instance.onItemChangedCallback += UpdateItemUI;
            Inventory.instance.onCardChangedCallback += UpdateCardUI;

            if (PhotonNetwork.IsMasterClient)
            {
                Inventory.instance.SendInventoryItems();
            }
        }


        private void Update()
        {
            inventoryItems = itemContainer.GetComponentsInChildren<InventoryItem>();
        }

        //public void UpdateItemUI()
        //{
        //    Debug.Log("UPDATING UI");

        //    for (int i = 0; i < inventoryItems.Length; i++)
        //    {
        //        if (i < inventory.items.Count)
        //        {
        //            inventoryItems[i].AddItem(inventory.items[i]);
        //        }
        //        else
        //        {
        //            inventoryItems[i].ClearItem();
        //        }
        //    }
        //}


        public void UpdateCardUI()
        {

            photonView.RPC("UpdateCardUIALL", RpcTarget.All);

        }

        [PunRPC]
        public void UpdateCardUIALL()
        {
            Debug.Log("UPDATING UI");

            for (int i = 0; i < inventoryItems.Length; i++)
            {
                if (i < Inventory.instance.cards.Count)
                {
                    inventoryItems[i].AddCard(Inventory.instance.cards[i]);
                }
                else
                {
                    inventoryItems[i].ClearCard();
                }
            }
        }
        
        [PunRPC]
        public void AddP2Card(int i)
        {
            inventoryItems[i].AddCard(Inventory.instance.cards[i]);
        }
        [PunRPC]
        public void RemoveP2Card(int i)
        {
            inventoryItems[i].AddCard(Inventory.instance.cards[i]);
        }
    }
}

