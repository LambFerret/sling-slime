using System;
using TMPro;
using UnityEngine;

namespace ui
{
    public class SpeedUI : MonoBehaviour
    {
        private TextMeshProUGUI _text;

        private void Awake()
        {
            _text = GetComponent<TextMeshProUGUI>();
        }

        private void SetSpeed(float speed)
        {
            _text.text = speed.ToString("0.00");
        }

        public void UpdateSpeed(Component sender, object data)
        {
            if (data is not float i) return;
            SetSpeed(i);
        }
    }
}