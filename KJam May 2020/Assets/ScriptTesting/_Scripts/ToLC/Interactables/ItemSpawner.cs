using Photon.Pun;
using System.Linq;
using ToLC.Interactables.Items;
using ToLC.Player;
using UnityEngine;

namespace ToLC.Interactables
{
    public class ItemSpawner : MonoBehaviourPun
    {
        [SerializeField] private LootSet lootSet = null;

        public bool onlySpawnOnce = true;

        private bool hasSpawned = false;

        [Range(0, 100)]
        public int addDropChance = 0;

        private void Start()
        {
            //photonView.RPC("SpawnLoot", RpcTarget.MasterClient);
            //SpawnLoot();
        }

        [PunRPC]
        public void SpawnLoot()
        {
            if (onlySpawnOnce)
            {
                if (!hasSpawned)
                {
                    photonView.RPC("Spawn", RpcTarget.MasterClient);
                   
                }
            }
            else
            {
                photonView.RPC("Spawn", RpcTarget.MasterClient);
                
            }

        }

        [PunRPC]
        public void Spawn()
        {
            if (lootSet.Loot.standardLoot.Count() > 0)
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
            }
            if (lootSet.Loot.cardLoot.Count() > 0)
            {
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
            hasSpawned = true;
        }
    }
}

