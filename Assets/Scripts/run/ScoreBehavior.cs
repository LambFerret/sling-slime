using System;
using events;
using ScriptableObjects;
using UnityEngine;

namespace run
{
    public class ScoreBehavior : MonoBehaviour
    {
        private Collider2D _col;

        public Score score;

        private void Awake()
        {
            _col = GetComponent<Collider2D>();
            GameEventManager.Instance.OnScoreChanged += ScoreChanged;
            GameEventManager.Instance.OnMoneyChanged += MoneyChanged;
            GameEventManager.Instance.OnSizeChanged += SizeChanged;
        }

        private void Start()
        {
            GetComponent<SpriteRenderer>().sprite = score.sprite;
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag("Player"))
            {
                GameEventManager.Instance.ScoreChanged(score.value);
            }
        }


        private void ScoreChanged(int value)
        {
            _col.enabled = true;
        }

        private void MoneyChanged(int value)
        {
            _col.enabled = false;
        }

        private void SizeChanged(float value)
        {
            _col.enabled = false;
        }

        private void OnDestroy()
        {
            GameEventManager.Instance.OnScoreChanged -= ScoreChanged;
            GameEventManager.Instance.OnMoneyChanged -= MoneyChanged;
            GameEventManager.Instance.OnSizeChanged -= SizeChanged;
        }
    }
}