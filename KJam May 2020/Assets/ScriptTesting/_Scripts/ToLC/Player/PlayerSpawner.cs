using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ToLC.Network
{
    public class PlayerSpawner : MonoBehaviour
    {
        public static PlayerSpawner instance;

        [SerializeField] private GameObject playerPrefab = null;
        private GameObject playerUser = null;

        [SerializeField] private Transform spawnPos1 = null;
        [SerializeField] private Transform spawnPos2 = null;

        private Transform spawnPos = null;


        private void Awake()
        {
            instance = this;
        }

        private void Start()
        {
            //GameObject go = PhotonNetwork.InstantiateSceneObject("Inventory", transform.position, Quaternion.identity);

            if (PhotonNetwork.IsMasterClient)
            {
                spawnPos = spawnPos1;
            }
            else
            {
                spawnPos = spawnPos2;
            }

            StartCoroutine(Spawn());
        }

        private IEnumerator Spawn()
        {
            yield return new WaitForEndOfFrame();
            playerUser = PhotonNetwork.Instantiate(playerPrefab.name, spawnPos.position, Quaternion.identity);
        }
    }
}

