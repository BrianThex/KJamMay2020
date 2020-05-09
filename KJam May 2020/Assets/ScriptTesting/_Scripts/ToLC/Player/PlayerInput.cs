using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using System;

namespace ToLC.Player
{
	public class PlayerInput : MonoBehaviourPun
	{
		public static PlayerInput instance;

		public bool isChest = true;

		public Interactable focus = null;

		private Camera cam = null;

		public enum InputState
		{
			GamePlay,
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
		}

		private void Update()
		{
			if (photonView.IsMine)
			{
				switch (inputState)
				{
					case InputState.GamePlay:
						//Movement.instance.HandleMovement();
						GamePlayMouseInput();
						break;
					case InputState.Paused:
						break;
				}
			}
		}

		private void GamePlayMouseInput()
		{
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

