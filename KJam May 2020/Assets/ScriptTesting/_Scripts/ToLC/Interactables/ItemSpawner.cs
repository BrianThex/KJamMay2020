using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using ToLC.Interactables.Items;
using ToLC.Player;
using UnityEngine;

namespace ToLC.Interactables
{
    public class ItemSpawner : MonoBehaviourPun
    {
        [SerializeField] private LootSet lootSet;

        [Range(0, 100)]
        public int addDropChance = 0;

        private void Start()
        {
            //photonView.RPC("SpawnLoot", RpcTarget.MasterClient);
            SpawnLoot();
        }

        [PunRPC]
        public void SpawnLoot()
        {
            foreach (Item item in lootSet.Loot.standardLoot)
            {

                GameObject loot = PhotonNetwork.Instantiate(item.prefab.name, new Vector3(transform.position.x, 2, transform.position.z), Quaternion.Euler(-90, 0, 0));
                Rigidbody rb = loot.GetComponent<Rigidbody>();
                ItemPickup ip = loot.GetComponent<ItemPickup>();
                ip.item = item;
                //rb.AddExplosionForce(20, new Vector3(transform.position.x, 2, transform.position.z), 5, 10);
                rb.AddForce(Vector3.up * 10, ForceMode.Impulse);
                Debug.Log("Loot obj to spawn" + loot.name);
            }

            foreach (Card card in lootSet.Loot.cardLoot)
            {
                GameObject loot = PhotonNetwork.Instantiate(card.prefab.name, new Vector3(transform.position.x, 2, transform.position.z), Quaternion.Euler(-90, 0, 0));
                Rigidbody rb = loot.GetComponent<Rigidbody>();
                ItemPickup ip = loot.GetComponent<ItemPickup>();
                ip.card = card;
                rb.AddForce(Vector3.up * 10, ForceMode.Impulse);
                //rb.AddExplosionForce(20, new Vector3(transform.position.x, 2, transform.position.z), 5, 10);
                Debug.Log("Loot obj to spawn" + loot.name);
            }
        }
    }
}

