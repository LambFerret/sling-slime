using System;
using core;
using run;
using UnityEngine;

namespace item
{
    public class Item : MonoBehaviour
    {
        public string itemName;
        public string itemDescription;
        public Sprite sprite;
        public int spawnChance;
        public bool isUnlocked;
        public float duration;

        private GameEvent _onItemGet;

        private void Awake()
        {
            _onItemGet = Resources.Load<GameEvent>("ScriptableObjects/event/GetItem");
        }

        public virtual void Use()
        {
            // impl
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.GetComponent<PlayerBehavior>() == null) return;
            Debug.Log("trigger entered");
            _onItemGet.Raise(this, this);
            gameObject.SetActive(false);
        }
    }
}