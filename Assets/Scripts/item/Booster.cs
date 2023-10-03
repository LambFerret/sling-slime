using UnityEngine;

namespace item
{
    public class Booster : Item
    {
        public float boostPower;
        public float duration;

        public override void Use()
        {
            base.Use();
            Debug.Log("Using " + itemName);
        }
    }
}