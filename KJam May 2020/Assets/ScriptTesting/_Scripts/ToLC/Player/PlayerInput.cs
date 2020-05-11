using System.Collections.Generic;
using ToLC.Interactables;
using UnityEngine;
using Photon.Pun;
using ToLC.Enemy;

namespace ToLC.Player
{
	public class PlayerInput : MonoBehaviourPun
	{
		public static PlayerInput instance;

		public HealthManager healthManager = null;

		[SerializeField] private Animator anim = null;

		public Interactable focus = null;

		private Camera cam = null;

		public float range = 20;

		public float damage = 5;

		public float attackCooldown = 0;

		public float baseCooldown = 0.5f;

		private bool isAttacking = false;

		private LayerMask enemyLayerMask;

		public enum InputState
		{
			GamePlay,
			Attacking,
			Paused
		}

		public InputState inputState;

		private void Awake()
		{
			instance = this;
		}

		private void Start()
		{
			inputState = InputState.GamePlay;
			cam = Camera.main;
			enemyLayerMask = LayerMask.NameToLayer("Enemy");
		}

		private void Update()
		{

			if (isAttacking && inputState == InputState.GamePlay)
			{
				inputState = InputState.Attacking;
			}
			else
			{
				inputState = InputState.GamePlay;
			}

			if (photonView.IsMine)
			{
				switch (inputState)
				{
					case InputState.GamePlay:
						//Movement.instance.HandleMovement();
						GamePlayMouseInput();
						break;
					case InputState.Attacking:
						break;
					case InputState.Paused:
						break;
				}
			}
		}

		private void GamePlayMouseInput()
		{
			attackCooldown -= Time.deltaTime;

			if (Input.GetMouseButtonDown(0))
			{
				RemoveFocus();

				Ray ray = cam.ScreenPointToRay(Input.mousePosition);
				RaycastHit hit;

				if (Physics.Raycast(ray, out hit))
				{
					Interactable interactable = hit.collider.GetComponent<Interactable>();

					if (interactable != null)
					{
						SetFocus(interactable);
					}
				}
			}

			if (Input.GetMouseButtonDown(1))
			{
				if (attackCooldown <= 0)
				{
					//isAttacking = true;
					anim.SetTrigger("horAttack");
					//int animation = UnityEngine.Random.Range(1, 2);

					//switch (animation)
					//{
					//	case 1:
					//		Movement.instance.anim.SetBool("dwnAttack", true);
					//		break;
					//	case 2:
					//		Movement.instance.anim.SetBool("horAttack", true);
					//		break;
					//}

					List<Enemy.AI> enemies = new List<Enemy.AI>();

					Quaternion startingAngle = Quaternion.AngleAxis(-60, Vector3.up);
					Quaternion stepAngle = Quaternion.AngleAxis(5, Vector3.up);

					RaycastHit hit;
					Quaternion angle = transform.rotation * startingAngle;
					Vector3 direction = angle * Vector3.forward;
					var pos = new Vector3(transform.position.x, .5f, transform.position.z);
					for (var i = 0; i < 24; i++)
					{
						//Debug.DrawRay(pos, direction * range, Color.red, 100);

						if (Physics.Raycast(pos, direction, out hit, range))
						{
							var enemy = hit.collider.GetComponent<Enemy.AI>();
							if (enemy)
							{
								//Enemy takes damage
								if (!enemies.Contains(enemy))
								{
									enemies.Add(enemy);
									Debug.Log(enemy.transform.gameObject.name);
								}
							}
						}
						direction = stepAngle * direction;
					}
					for (int e = 0; e < enemies.Count; e++)
					{
						enemies[e].healthManager.TakeDamage(damage);
					}
					attackCooldown = baseCooldown;
				}
			}
		}

		private void RemoveFocus()
		{	
			if (focus != null)
				focus.OnDefocused();
			focus = null;
		}

		[PunRPC]
		public void SetFocus(Interactable newFocus)
		{
			if (newFocus != focus)
			{
				if (focus != null)
					focus.OnDefocused();
				focus = newFocus;
			}
				InteractFocus(newFocus);
		}

		[PunRPC]
		public void InteractFocus(Interactable newFocus)
		{
			int ID = this.photonView.ViewID;
			GameObject playerEntity = PhotonView.Find(ID).transform.gameObject;
			newFocus.OnFocused(playerEntity.transform);
		}
	}
}

