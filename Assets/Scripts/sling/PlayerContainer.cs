using System;
using System.Collections.Generic;
using core;
using persistence;
using persistence.data;
using ScriptableObjects;
using UnityEngine;

namespace sling
{
    public class PlayerContainer : MonoBehaviour, IDataPersistence
    {
        public List<GameObject> slimeObjects;
        public GameObject slimePrefab;


        private List<string> _slimesId;

        private void Start()
        {
            foreach (var slimeId in _slimesId)
            {
                AddSlime(slimeId);
            }
        }

        private void AddSlime(string id)
        {
            var slime = GameDataManager.Instance.slimeDictionary[id];
            var slimeObject = Instantiate(slimePrefab, transform);
            slimeObject.GetComponent<PlayableSlime>().slime = slime;
            Debug.Log(slime.ID);

            //change color by type of slime
            var slimeRenderer = slimeObject.GetComponent<SpriteRenderer>();
            switch (slime.slimeType)
            {
                case Slime.SlimeType.Wind:
                    slimeRenderer.color = Color.blue;
                    break;
                case Slime.SlimeType.Fire:
                    slimeRenderer.color = Color.cyan;
                    break;
                case Slime.SlimeType.Lightening:
                    slimeRenderer.color = Color.yellow;
                    break;
            }

            slimeObjects.Add(slimeObject);
        }

        public void AddSlime(Slime slime)
        {
            var slimeObject = Instantiate(slimePrefab, transform);
            slimeObject.GetComponent<PlayableSlime>().slime = slime;
            // red color
            slimeObject.GetComponent<SpriteRenderer>().color = Color.red;
            slimeObjects.Add(slimeObject);
        }

        public void LoadData(GameData data)
        {
            _slimesId = data.slimeIdList;
        }

        public void SaveData(GameData data)
        {
        }
    }
}