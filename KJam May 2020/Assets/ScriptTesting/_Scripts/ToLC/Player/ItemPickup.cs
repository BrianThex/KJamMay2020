using Photon.Pun;
using System.Collections;
using ToLC.Interactables.Items;
using ToLC.Interactables;
using UnityEngine;

namespace ToLC.Player
{
    public class ItemPickup : Interactable
    {
        public Item item = null;
        public Card card = null;
        public override void Interact()
        {
            base.Interact();
            //photonView.RPC("PickUp", RpcTarget.All, gameObject);
            StartCoroutine(DestroyMe());
        }

        private IEnumerator GetOwnerShip()
        {
            //photonView.RequestOwnership();
            yield return new WaitForEndOfFrame();
        }

        private IEnumerator DestroyMe()
        {
            yield return StartCoroutine(GetOwnerShip());

            //if (photonView.IsMine)
            //{
            //    if (item != null)
            //    {
            //        Debug.Log("Player Picked up " + item.name);
            //        bool hasPickedUp = Inventory.Inventory.instance.Add(item);
            //        if (hasPickedUp)
            //            PhotonNetwork.Destroy(gameObject);
            //    }
            //    if (card != null)
            //    {
            //        Debug.Log("Player Picked up " + card.name);
            //        bool hasPickedUp = Inventory.Inventory.instance.Add(card);
            //        if (hasPickedUp)
            //            PhotonNetwork.Destroy(gameObject);
            //    }
            //}
            //else
            //    StartCoroutine(DestroyMe());

            if (item != null)
            {
                Debug.Log("Player Picked up " + item.name);
                bool hasPickedUp = Inventory.Inventory.instance.Add(item);
                if (hasPickedUp)
                    Destroy(gameObject);
            }
            if (card != null)
            {
                Debug.Log("Player Picked up " + card.name);
                bool hasPickedUp = Inventory.Inventory.instance.Add(card);
                if (hasPickedUp)
                    Destroy(gameObject);
            }
        }

        //[PunRPC]
        //private void PickUpItem()
        //{
        //    Inventory.Inventory.instance.Add(item);
        //}

        //[PunRPC]
        //private void PickUpCard()
        //{
        //    Inventory.Inventory.instance.Add(card);
        //}
    }
}

