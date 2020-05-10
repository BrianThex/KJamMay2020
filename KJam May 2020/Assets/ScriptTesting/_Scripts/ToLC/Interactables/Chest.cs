using UnityEngine;

namespace ToLC.Interactables
{
    public class Chest : Interactable
    {
        public ItemSpawner spawner = null;
        public override void Interact()
        {
            base.Interact();
            spawner.SpawnLoot();
        }
    }
}

