using Photon.Pun;
using System.Collections;
using ToLC.Interactables.Items;
using ToLC.Interactables;
using UnityEngine;
using ToLC.Player.Inventory;

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
			photonView.RPC("DestroyMe", RpcTarget.All);
        }


        private IEnumerator GetOwnerShip()
        {
            //photonView.RequestOwnership();
            yield return new WaitForEndOfFrame();
        }

		[PunRPC]
		public IEnumerator DestroyMe()
        {
            yield return StartCoroutine(GetOwnerShip());

			#region Mixing Animations Later?

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

			#endregion

			//if (item != null)
			//{
			//    Debug.Log("Player Picked up " + item.name);
			//    bool hasPickedUp = Inventory.Inventory.instance.Add(item);
			//    if (hasPickedUp)
			//        Destroy(gameObject);
			//}
			if (card != null)
			{
				if (photonView.IsMine)
				{
					Debug.Log("Player Picked up " + card.name);
					bool hasPickedUp = Inventory.Inventory.instance.Add(card);

					if (hasPickedUp)
					{
						if (photonView.IsMine)
						{
							PhotonNetwork.Destroy(gameObject);
						}
					}
				}
				else
				{
					Debug.Log("I didn't add the card");
				}

			}
		}
	}
}

