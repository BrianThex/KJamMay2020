using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeleteMe : MonoBehaviour
{
	public GameObject cube = null;
	public Transform cubeSpawn = null;
	private void Start()
	{
		PhotonNetwork.InstantiateSceneObject(cube.name, cubeSpawn.position, Quaternion.identity);
	}
}
