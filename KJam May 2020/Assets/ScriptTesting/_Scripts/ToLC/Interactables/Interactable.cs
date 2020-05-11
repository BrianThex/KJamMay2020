using Photon.Pun;
using ToLC.Player;
using ToLC.Player.Inventory;
using UnityEngine;

namespace ToLC.Interactables
{
    public class Interactable : MonoBehaviourPun
    {
        public float radius = 3f;

        private bool isFocus = false;

        private Transform player = null;

        private bool hasInteracted = false;

        [PunRPC]
        public virtual void Interact()
        {
            // This will be for our over writes

        }

        private void Update()
        {

        }

        public void OnFocused(Transform playerTransform )
        {
            isFocus = true;
            player = playerTransform;
            hasInteracted = false;

            if (isFocus && !hasInteracted)
            {
                Debug.Log("About to test Distance!");
                Debug.Log($"Player Pos: {player.position} \n Chest Pos: {transform.position}");

                float distance = Vector3.Distance(player.position, transform.position);
                Debug.Log($"Distance : {distance}");

                if (distance <= radius)
                {
                    Debug.Log("INTERACTING!");
                    Interact();
                    hasInteracted = true;
                }
            }
        }

        public void OnDefocused()
        {
            isFocus = false;
            player = null;
            hasInteracted = false;
        }

        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireSphere(transform.position, radius);
        }
    }
}

