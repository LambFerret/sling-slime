using System;
using System.Collections.Generic;
using components;
using item;
using UnityEngine;

namespace run
{
    public class ItemSpawner : MonoBehaviour
    {
        public int spawnChance;
        public List<Item> items;
        public GameObject shelf;

        private float _totalSpawnChance;
        private List<GameObject> _itemPool;

        private void Start()
        {
            _itemPool = new List<GameObject>();
            foreach (Item item in items)
            {
                _totalSpawnChance += item.spawnChance;
                for (int i = 0; i < item.spawnChance; i++)
                {
                    GameObject obj = Instantiate(item.gameObject, shelf.transform);
                    obj.SetActive(false);
                    _itemPool.Add(obj);
                }
            }
        }

        public GameObject SpawnRandomItem(GameObject newShelf = null)
        {
            float random = UnityEngine.Random.Range(0, _totalSpawnChance);
            float current = 0;
            foreach (Item item in items)
            {
                current += item.spawnChance;
                if (random <= current)
                {
                    foreach (GameObject obj in _itemPool)
                    {
                        if (obj.activeInHierarchy) continue;
                        if (newShelf is not null)
                        {
                            obj.transform.SetParent(newShelf.transform);
                        }
                        return obj;
                    }
                }
            }

            return null;
        }
    }
}