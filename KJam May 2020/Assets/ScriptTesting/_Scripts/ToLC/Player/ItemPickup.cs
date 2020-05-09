using Photon.Pun;
using System.Collections;
using ToLC.Items;
using UnityEngine;

using ToLC.Player.Inventory;

namespace ToLC.Player
{
    public class ItemPickup : Interactable
    {
        public Item item = null;
        public override void Interact()
        {
            base.Interact();
            //photonView.RPC("PickUp", RpcTarget.All, gameObject);
            StartCoroutine(DestroyMe());
        }

        private IEnumerator GetOwnerShip()
        {
            photonView.RequestOwnership();
            yield return new WaitForEndOfFrame();
        }

        private IEnumerator DestroyMe()
        {
            yield return StartCoroutine(GetOwnerShip());

            if (photonView.IsMine)
            {
                Debug.Log("Player Picked up " + item.name);
                bool wasPickedUp = Inventory.Inventory.instance.Add(item);

                if(wasPickedUp)
                    PhotonNetwork.Destroy(gameObject);
            }
            else
                StartCoroutine(DestroyMe());
        }
    }
}

