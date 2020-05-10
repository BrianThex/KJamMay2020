using System.Collections.Generic;
using ToLC.Interactables;
using UnityEngine;
using Photon.Pun;
using System;
using System.Linq;
using ToLC.Enemy;
using TMPro;

namespace ToLC.Player
{
	public class PlayerInput : MonoBehaviourPun
	{
		public static PlayerInput instance;

		[SerializeField] private Animator anim = null;

		public bool isChest = true;

		public Interactable focus = null;

		private Camera cam = null;

		public float currentHealth = 0;
		public float health = 100;

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
			currentHealth = health;
		}

		private void Update()
		{
			if (currentHealth <= 0)
			{
				Die();
			}

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

		private void Die()
		{
			Destroy(gameObject);
		}

		public void TakeDamage(float damage)
		{
			currentHealth = currentHealth - damage;
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

					List<AI> enemies = new List<AI>();

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
							var enemy = hit.collider.GetComponent<AI>();  
							if (enemy)
							{
								//Enemy takes damage
								if (!enemies.Contains(enemy))
								{
									enemies.Add(enemy);
									Debug.Log(enemy.transform.gameObject.name);
									enemy.TakeDamage(damage);
								}
							}
						}
						direction = stepAngle * direction;
					}
					attackCooldown = baseCooldown;
					isAttacking = false;
				}
			}
		}

		private void RemoveFocus()
		{	
			if (focus != null)
				focus.OnDefocused();
			focus = null;
		}

		private void SetFocus(Interactable newFocus)
		{
			if (newFocus != focus)
			{
				if (focus != null)
					focus.OnDefocused();
				focus = newFocus;
			}
			newFocus.OnFocused(gameObject.transform);
		}
	}
}

