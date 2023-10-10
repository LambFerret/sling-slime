using System.Collections.Generic;
using ScriptableObjects;
using UnityEngine;

namespace core
{
    public class GameDataManager : Singleton<GameDataManager>
    {
        public Dictionary<string, Slime> slimeDictionary;
        public Slime selectedSlime;

        protected override void Awake()
        {
            base.Awake();
            slimeDictionary = new Dictionary<string, Slime>();
            var slimes = Resources.LoadAll<Slime>($"ScriptableObjects/slime/");
            foreach (Slime slime in slimes)
            {
                slimeDictionary.Add(slime.ID, slime);
            }
        }
    }
}