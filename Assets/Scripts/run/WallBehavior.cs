using System;
using System.Collections;
using System.Collections.Generic;
using core;
using persistence;
using persistence.data;
using UnityEngine;

namespace run
{
    public class WallBehavior : MonoBehaviour, IDataPersistence
    {
        public float maxForce = 10f;
        public float powerCorrection = 1f;

        private DistanceJoint2D _joint;
        private LineRenderer _line;
        private Transform _player;

        public bool isSuccess;

        public void LoadData(GameData data)
        {
            maxForce = data.wallBreakForce;
            powerCorrection = data.powerCorrectionIntoObstacle;
        }

        public void SaveData(GameData data)
        {
        }

        private void Start()
        {
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
                var power = _player.GetComponent<PlayerBehavior>().power;

                // 충돌힘 + 파워 * 파워보정치 > 임계점
                isSuccess = impactForce + power * powerCorrection > maxForce;
            }
        }


        private void OnTriggerExit2D(Collider2D other)
        {
            if (other.CompareTag("Player"))
            {
                // GameManager.instance.ChangeZoom(30F);
                _player = null;
                _line.enabled = false;
                if (!isSuccess)
                {
                    GameManager.instance.GameOver();
                }
                else
                {
                    Time.timeScale = 1f;
                    GameManager.instance.AddPlayerSpeed(-maxForce);
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