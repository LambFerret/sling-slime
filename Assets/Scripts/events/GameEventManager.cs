using System;
using UnityEngine;

namespace events
{
    public class GameEventManager : MonoBehaviour
    {
        public static GameEventManager Instance { get; private set; }

        private void Awake()
        {
            if (Instance != null) Debug.LogError("Found more than one Game Events Manager in the scene.");
            Instance = this;
        }


        public event Action<float> OnPopularityChanged;

        public void PopularityChanged(float value)
        {
            OnPopularityChanged?.Invoke(value);
        }

        public event Action<int> OnMoneyChanged;

        public void MoneyChanged(int value)
        {
            OnMoneyChanged?.Invoke(value);
        }

    }
}