using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ToLC.Network
{
    public class PlayerSpawner : MonoBehaviour
    {
        [SerializeField] private GameObject playerPrefab = null;
        private GameObject playerUser = null;

        private void Start()
        {
            playerUser = PhotonNetwork.Instantiate(playerPrefab.name, Vector3.zero, Quaternion.identity);
        }
    }
}

