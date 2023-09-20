using UnityEngine;

namespace run
{
    public class AIMoving : MonoBehaviour
    {
        public float maxSpeed = 20.0f;
        public float minSpeed = 5.0f;
        private Rigidbody2D _rb;

        private float _speed;
        private void Start()
        {
            _rb = GetComponent<Rigidbody2D>();
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