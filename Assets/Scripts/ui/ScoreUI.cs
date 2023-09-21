using System;
using TMPro;
using UnityEngine;

namespace ui
{
    public class ScoreUI :MonoBehaviour
    {

        private TextMeshProUGUI _text;

        private void Awake()
        {
            _text = GetComponent<TextMeshProUGUI>();
            SetScore(0);
        }

        private void SetScore(int score)
        {
            _text.text = score.ToString();
        }

        public void UpdateScore(Component sender, object data)
        {
            Debug.Log("Score update called ");
            if (data is not int i) return;
            SetScore(i);
        }

    }
}