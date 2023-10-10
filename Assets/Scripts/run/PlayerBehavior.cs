using System;
using core;
using persistence;
using persistence.data;
using ScriptableObjects;
using UnityEngine;

namespace run
{
    public class PlayerBehavior : MonoBehaviour, IDataPersistence
    {
        [HideInInspector] public Rigidbody2D rb;

        [Header("Player Settings")]
        // slime 관련 (이게 가면)
        public Slime slime;

        [Header("Status Report")]
        // public float jumpForce = 5f;
        public float speed;

        public float health;
        public float power;

        [Header("Status Multiplier")] public float speedMultiplier = 3F;
        public float healthMultiplier = 4F;
        public float powerMultiplier = 5F;

        [Header("Config")] public float speedDownByGround = 0.1f;
        public float speedDownByTime = 0.1F;
        public float healthDownByTime = 0.1F;
        public float empowerMultiplier = 1F;
        public float defensePower = 1F;

        [Header("Events")] public GameEvent onPlayerScoreChanged;
        public GameEvent onPlayerSpeedChanged;
        public GameEvent onPlayerHealthChanged;


        public void LoadData(GameData data)
        {
            speedMultiplier = data.speedMultiplier;
            healthMultiplier = data.healthMultiplier;
            powerMultiplier = data.powerMultiplier;
            speedDownByGround = data.speedDownByGround;
            speedDownByTime = data.speedDownByTime;
            healthDownByTime = data.healthDownByTime;
            empowerMultiplier = data.empowerMultiplier;
            defensePower = data.defensePower;
        }

        public void SaveData(GameData data)
        {
        }

        private void Awake()
        {
            rb = GetComponent<Rigidbody2D>();

            slime = GameDataManager.Instance.selectedSlime;
            CalculateStatus();

            switch (slime.slimeType)
            {
                case Slime.SlimeType.Wind:
                    speedDownByTime *= slime.MultiplyByType();
                    break;
                case Slime.SlimeType.Fire:
                    empowerMultiplier *= slime.MultiplyByType();
                    break;
                case Slime.SlimeType.Lightening:
                    defensePower *= slime.MultiplyByType();
                    break;
            }
        }

        private void CalculateStatus()
        {
            speed = slime.speed * speedMultiplier;
            health = slime.health * healthMultiplier;
            power = slime.power * powerMultiplier;
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
            speed -= speedDownByTime * Time.deltaTime;
            if (speed <= 0.1F)
            {
                GameManager.instance.GameOver();
            }

            // this object scale size is following the health
            var a = health / 10;
            transform.localScale = new Vector3(a, a, a);
            // cine camera follow this object and size
            GameManager.instance.ChangeZoom(10F + a);

            health -= healthDownByTime * Time.deltaTime;
            if (health <= 0.1F)
            {
                GameManager.instance.GameOver();
            }

            onPlayerSpeedChanged.Raise(this, speed);
            onPlayerHealthChanged.Raise(this, health);
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.GetComponent<ScoreBehavior>() == null) return;
            Score score = other.GetComponent<ScoreBehavior>().score;
            onPlayerScoreChanged.Raise(this, score.scoreValue);

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