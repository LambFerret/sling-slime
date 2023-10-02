using System;
using System.Collections;
using Cinemachine;
using run;
using UnityEngine;

namespace core
{
    public class GameManager : MonoBehaviour
    {
        public static GameManager instance;

        public PlayerBehavior player;
        public CinemachineVirtualCamera virtualCamera;

        public float playerSpeed;

        private float _currentZoom;
        private float _targetZoom;

        private void Awake()
        {
            if (instance == null)
            {
                instance = this;
            }
        }

        private void Start()
        {
            playerSpeed = player.speed;

            _currentZoom = _targetZoom = virtualCamera.m_Lens.OrthographicSize;
        }

        private void Update()
        {
            // player speed
            player.speed -= Time.deltaTime * 0.1f;
            playerSpeed = player.speed;

            // cinematic camera
            if (Math.Abs(_currentZoom - _targetZoom) > 2F) _currentZoom = Mathf.Lerp(_currentZoom, _targetZoom, 0.01f);

            virtualCamera.m_Lens.OrthographicSize = _currentZoom;

            // debug input
            if (Input.GetKeyDown(KeyCode.Q))
            {
                //jump
                Vector3 jump = Vector3.up * player.jumpForce;
                player.rb.AddForce(jump, ForceMode2D.Impulse);
            }

            if (Input.GetKeyDown(KeyCode.RightArrow))
            {
                player.speed += 10;
            }
        }

        public void ChangeZoom(float value)
        {
            _targetZoom = value;
        }
    }
}