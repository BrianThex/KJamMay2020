using UnityEngine;

namespace ToLC.Interactables.Items
{
    [CreateAssetMenu(fileName = "New Item", menuName = "Inventory/Item")]
    public class Item : ScriptableObject
    {
        new public string name = "New Item";
        public Sprite icon = null;
        public bool isDefaultItem = false;
        public bool isStackable = false;
    }

    [CreateAssetMenu(fileName = "New Item", menuName = "Inventory/Card")]
    public class Card : ScriptableObject
    {
        new public string name = "New Card";
        public Sprite artwork = null;
        public bool isDefaultItem = false;
        public ParticleSystem Particles = null;

        public float damage = 0;
        public float cooldown = 0;
        public float aoeRange = 0;
    }
}

