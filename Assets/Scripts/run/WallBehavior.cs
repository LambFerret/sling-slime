using System;
using System.Collections;
using System.Collections.Generic;
using core;
using UnityEngine;

namespace run
{
    public class WallBehavior : MonoBehaviour
    {
        public float maxForce = 10f; // The force at which the object will "break" free from the wall.

        private DistanceJoint2D _joint;
        private Rigidbody2D _rb;
        private LineRenderer _line;

        private Transform _player;

        public bool isSuccess;

        private void Start()
        {
            _rb = GetComponent<Rigidbody2D>();
            _joint = GetComponent<DistanceJoint2D>();
            _line = GetComponent<LineRenderer>();
            _joint.enabled = false;
            _line.enabled = false;
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag("Player"))
            {
                float impactForce = GameManager.instance.playerSpeed;
                _line.enabled = true;
                _player = other.transform;
                Time.timeScale = 0.1f;
                // scale up camera
                GameManager.instance.ChangeZoom(10F);

                isSuccess = impactForce > maxForce;
            }
        }



        private void OnTriggerExit2D(Collider2D other)
        {
            if (other.CompareTag("Player"))
            {
                GameManager.instance.ChangeZoom(30F);
                _player = null;
                _line.enabled = false;
                Time.timeScale = 1f;
                if (!isSuccess)
                {
                    // GameOver
                    Debug.Log("Game Over");
                }
            }
        }

        private void Update()
        {
            _line.SetPosition(0, transform.position);
            // if player null, disable line
            if (_player is null) return;
            _line.SetPosition(1, _player.position);

        }
    }
}