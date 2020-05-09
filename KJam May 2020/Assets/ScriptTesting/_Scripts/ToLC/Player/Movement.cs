using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace ToLC.Player
{
    [RequireComponent(typeof(CharacterController))]
    public class Movement : MonoBehaviourPun
    {
        public static Movement instance;

        [SerializeField] private float moveSpeed = 0f;

        [SerializeField] Transform playerGraphics = null;

        private CharacterController controller = null;

        private void Awake()
        {
            instance = this;
            controller = GetComponent<CharacterController>();
        }

        private void Start()
        {

        }

        private void Update()
        {
            if (photonView.IsMine)
            {
                HandleMovement();
            }
        }

        public void HandleMovement()
        {
            Vector3 movement = new Vector3
            {
                x = Input.GetAxisRaw("Horizontal"),
                y = 0f,
                z = Input.GetAxisRaw("Vertical")
            }.normalized;

            controller.SimpleMove(movement * moveSpeed);

            Camera cam = Camera.main;
            cam.transform.position = new Vector3(transform.position.x, transform.position.y + 25, transform.position.z - 12);
            cam.transform.LookAt(controller.transform.position);

            if (movement != Vector3.zero)
            {
                transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(movement), 10 * Time.deltaTime);
            }
        }
    }
}