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
        private GameObject _prefab;

        private void Awake()
        {
            _col = GetComponent<Collider2D>();
        }

        private void Start()
        {
            GameEventManager.Instance.OnScoreChanged += ScoreChanged;
            GameEventManager.Instance.OnMoneyChanged += MoneyChanged;
            GameEventManager.Instance.OnSizeChanged += SizeChanged;
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag("Player"))
            {
                GameEventManager.Instance.ScoreChanged(score.value);
                Vector2 dir = score.scoreType == Score.ScoreType.Flying ? Vector2.right : Vector2.up;
                other.GetComponent<PlayerBehavior>().rb.AddForce(dir * score.value, ForceMode2D.Impulse);
                gameObject.SetActive(false);
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