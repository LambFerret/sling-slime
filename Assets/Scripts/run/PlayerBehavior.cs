using System;
using UnityEngine;

namespace run
{
    public class PlayerBehavior : MonoBehaviour
    {
        [HideInInspector] public Rigidbody2D rb;
        public float jumpForce = 5f;
        public float speed = 30f;

        private void Awake()
        {
            rb = GetComponent<Rigidbody2D>();
        }

        private void Update()
        {
            if (Input.GetKey(KeyCode.Q))
            {
                rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            }
        }
    }
}