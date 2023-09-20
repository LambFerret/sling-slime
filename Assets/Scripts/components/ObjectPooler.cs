using System.Collections.Generic;
using UnityEngine;

namespace components
{
    public class ObjectPooler : MonoBehaviour
    {
        public List<GameObject> prefabs;
        public int pooledAmount;
        public List<GameObject> pooledObjects;
        public GameObject shelf;
        public bool isRandom;

        private void Start()
        {
            pooledObjects = new List<GameObject>();
            for (int i = 0; i < pooledAmount; i++)
            {
                // if random, do random instantiate, if not, instantiate in order of prefabs
                // if i is greater than prefabs count, instantiate prefab from the beginning
                GameObject obj =
                    Instantiate(isRandom ? prefabs[Random.Range(0, prefabs.Count)] : prefabs[i % prefabs.Count],
                        shelf.transform);
                obj.SetActive(false);
                pooledObjects.Add(obj);
            }
        }

        public GameObject GetPooledObject(GameObject newShelf = null)
        {
            foreach (var t in pooledObjects)
            {
                if (!t.activeInHierarchy)
                {
                    if (newShelf is not null)
                    {
                        t.transform.SetParent(newShelf.transform);
                    }

                    return t;
                }
            }

            GameObject obj = Instantiate(isRandom ? prefabs[Random.Range(0, prefabs.Count)] : prefabs[0],
                newShelf is null ? shelf.transform : newShelf.transform);
            obj.SetActive(false);
            pooledObjects.Add(obj);
            return obj;
        }
    }
}