using Photon.Pun;
using Photon.Realtime;
using UnityEngine;

namespace ToLC
{
    public class HealthManager : MonoBehaviourPun
    {
		public bool isEnemy = true;
		public float currentHealth = 0;
		public float health = 100;
		private void Start()
		{
			currentHealth = health;
		}
		private void Update()
		{
			if (currentHealth <= 0)
			{
				Die();
			}
		}
		private void Die()
		{
			if (isEnemy)
				photonView.RPC("destroyObj", RpcTarget.All);

			else
				Destroy(gameObject);
		}

		public void TakeDamage(float damage)
		{
			currentHealth -= damage;
		}

		[PunRPC]
		public void destroyObj()
		{
			Destroy(gameObject);
		}
	}
}

