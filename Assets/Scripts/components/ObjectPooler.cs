using System.Collections.Generic;
using UnityEngine;

namespace components
{
    public class ObjectPooler : MonoBehaviour
    {
        public GameObject pooledObject;
        public int pooledAmount;
        public List<GameObject> pooledObjects;

        private void Start()
        {
            pooledObjects = new List<GameObject>();
            for (int i = 0; i < pooledAmount; i++)
            {
                GameObject obj = Instantiate(pooledObject);
                obj.SetActive(false);
                pooledObjects.Add(obj);
            }
        }

        public GameObject GetPooledObject()
        {
            for (int i = 0; i < pooledObjects.Count; i++)
            {
                if (!pooledObjects[i].activeInHierarchy)
                {
                    return pooledObjects[i];
                }
            }

            GameObject obj = Instantiate(pooledObject);
            obj.SetActive(false);
            pooledObjects.Add(obj);
            return obj;
        }
    }
}