using UnityEngine;

namespace ToLC.Interactables.Items
{
    [CreateAssetMenu(fileName = "New Card", menuName = "Inventory/Card")]
    public class Card : ScriptableObject
    {
        new public string name = "New Card";
        public GameObject prefab = null;
        public Sprite icon = null;
        public bool isDefaultItem = false;
        public ParticleSystem Particles = null;

        public float damage = 0;
        public float cooldown = 0;
        public float aoeRange = 0;
    }
}