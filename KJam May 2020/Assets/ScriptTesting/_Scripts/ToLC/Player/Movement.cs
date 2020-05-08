using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ToLC.Player
{
    [RequireComponent(typeof(CharacterController))]
    public class Movement : MonoBehaviourPun
    {
        public static Movement instance;

        [SerializeField] private float moveSpeed = 0f;

        private CharacterController controller = null;

        private void Awake()
        {
            instance = this;
        }

        private void Start()
        {
            controller = GetComponent<CharacterController>();
        }

        private void Update()
        {
            if (photonView.IsMine)
            {
                TakeInput();
            }
        }

        private void TakeInput()
        {
            Vector3 movement = new Vector3
            {
                x = Input.GetAxisRaw("Horizontal"),
                y = 0f,
                z = Input.GetAxisRaw("Vertical")
            }.normalized;

            controller.SimpleMove(movement * moveSpeed);
        }
    }
}