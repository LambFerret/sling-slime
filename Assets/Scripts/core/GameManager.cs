using System;
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
        }

        private void Update()
        {
            player.speed -= Time.deltaTime * 0.1f;
            playerSpeed = player.speed;

            if (Input.GetKeyDown(KeyCode.Q))
            {
                //jump
                Vector3 jump = Vector3.up * player.jumpForce;
                player.rb.AddForce(jump, ForceMode2D.Impulse);
            }
        }
    }
}