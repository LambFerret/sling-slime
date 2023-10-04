using System;
using core;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace ui
{
    public class HealthUI : MonoBehaviour
    {
        private TextMeshProUGUI _text;
        private Image _currentHealth;
        private float _fullHealth;

        private void Awake()
        {
            _text = transform.Find("text").GetComponent<TextMeshProUGUI>();
            _currentHealth = transform.Find("current").GetComponent<Image>();
        }

        private void Start()
        {
            _fullHealth = GameManager.instance.player.health;
        }

        private void SetHealth(float health)
        {
            _text.text = (int)health + " / " + (int)_fullHealth;
            _currentHealth.fillAmount = health / _fullHealth;
        }

        public void UpdateHealth(Component sender, object data)
        {
            if (data is not float i) return;
            SetHealth(i);
        }
    }
}