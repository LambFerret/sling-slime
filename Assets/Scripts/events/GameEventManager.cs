using System;
using UnityEngine;

namespace events
{
    public class GameEventManager : MonoBehaviour
    {
        private static GameEventManager Instance { get;  set; }

        private void Awake()
        {
            if (Instance != null) Debug.LogError("Found more than one Game Events Manager in the scene.");
            Instance = this;
        }


        public event Action<int> OnScoreChanged;
        public void ScoreChanged(int value)
        {
            OnScoreChanged?.Invoke(value);
        }

        public event Action<float> OnSpeedChanged;

        public void SpeedChanged(float value)
        {
            OnSpeedChanged?.Invoke(value);
        }

        public event Action<float> OnSizeChanged;

        public void SizeChanged(float value)
        {
            OnSizeChanged?.Invoke(value);
        }

        public event Action<int> OnMoneyChanged;

        public void MoneyChanged(int value)
        {
            OnMoneyChanged?.Invoke(value);
        }
    }
}