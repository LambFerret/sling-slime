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

        private void Update()
        {
            if (Input.GetKey(KeyCode.RightArrow))
            {
                playerSpeed = player.speed;
            }
            else
            {
                playerSpeed = 0;
            }
        }
    }
}