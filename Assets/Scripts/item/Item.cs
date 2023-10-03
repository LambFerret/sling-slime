using UnityEngine;

namespace item
{
    public class Item : MonoBehaviour
    {
        public string itemName;
        public string itemDescription;
        public Sprite sprite;
        public float spawnChance;
        public bool isUnlocked;

        public virtual void Use()
        {
            Debug.Log("Using " + itemName);
        }
    }
}