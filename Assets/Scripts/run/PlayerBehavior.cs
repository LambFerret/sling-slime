using System;
using core;
using events;
using ScriptableObjects;
using UnityEngine;

namespace run
{
    public class PlayerBehavior : MonoBehaviour
    {
        [HideInInspector] public Rigidbody2D rb;
        public float jumpForce = 5f;
        public float speed = 30f;
        public float speedDownByGround = 0.1f;

        [Header("Events")] public GameEvent onPlayerScoreChanged;
        public GameEvent onPlayerSpeedChanged;

        private void Awake()
        {
            rb = GetComponent<Rigidbody2D>();
        }

        private void OnCollisionEnter2D(Collision2D other)
        {
            if (other == null) return;

            if (other.gameObject.CompareTag("Ground"))
            {
                speed *= speedDownByGround;
            }
        }

        private void Update()
        {
            if (speed <= 0.1F) speed = 0.1F;
            onPlayerSpeedChanged.Raise(this, speed);
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.GetComponent<ScoreBehavior>() == null) return;
            Score score = other.GetComponent<ScoreBehavior>().score;
            onPlayerScoreChanged.Raise(this, score.value);

            switch (score.scoreType)
            {
                case Score.ScoreType.Air:
                    speed += score.forceAmount.x;
                    other.gameObject.SetActive(false);
                    break;
                case Score.ScoreType.Land:
                    break;
            }
        }
    }
}