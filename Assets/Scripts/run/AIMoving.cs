using persistence;
using persistence.data;
using UnityEngine;

namespace run
{
    public class AIMoving : MonoBehaviour, IDataPersistence
    {
        public float maxSpeed = 20.0f;
        public float minSpeed = 5.0f;
        private Rigidbody2D _rb;
        private float _speed;

        public void LoadData(GameData data)
        {
            maxSpeed = data.enemyMaxSpeed;
            minSpeed = data.enemyMinSpeed;
        }

        public void SaveData(GameData data)
        {
        }

        private void Start()
        {
            _rb = GetComponent<Rigidbody2D>();
        }

        public void Disable()
        {
            _rb.velocity = Vector2.zero;
            enabled = false;
        }

        private void OnEnable()
        {
            _speed = Random.Range(minSpeed, maxSpeed);
        }

        private void FixedUpdate()
        {
            _rb.velocity = new Vector2(_speed, _rb.velocity.y);
        }
    }
}