using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using run;
using ScriptableObjects;
using UnityEngine;

namespace core
{
    public class GameManager : MonoBehaviour
    {
        public static GameManager instance;

        public PlayerBehavior player;
        public CinemachineVirtualCamera virtualCamera;

        public float playerSpeed;

        public List<Slime> slimes;
        public Slime currentSlime;

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
            playerSpeed = player.speed;

            // cinematic camera
            if (Math.Abs(_currentZoom - _targetZoom) > 2F) _currentZoom = Mathf.Lerp(_currentZoom, _targetZoom, 0.01f);

            virtualCamera.m_Lens.OrthographicSize = _currentZoom;

            // debug input
            if (Input.GetKeyDown(KeyCode.Q)) Jump();
        }

        public void Jump()
        {
            Vector3 jump = Vector3.up * 5;
            player.rb.AddForce(jump, ForceMode2D.Impulse);
        }

        public void ChangeZoom(float value)
        {
            _targetZoom = value;
        }

        public void ChangePlayerSpeed(float value)
        {
            player.speed = value;
        }

        public void AddPlayerSpeed(float value)
        {
            player.speed += value;
        }
    }
}