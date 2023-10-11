using System;
using core;
using run;
using UnityEngine;

namespace item
{
    public abstract class Item : MonoBehaviour
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

        public abstract void OnGet();

        public abstract void OnUse();

        public abstract void OnEnd();

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.GetComponent<PlayerBehavior>() == null) return;
            _onItemGet.Raise(this, this);
            // gameObject.SetActive(false);
        }
    }
}