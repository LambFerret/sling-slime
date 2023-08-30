using System;
using events;
using ScriptableObjects;
using UnityEngine;

namespace run
{
    public class HookBehavior : MonoBehaviour
    {
        private TongueController _tongue;
        private PlayerBehavior _player;
        public DistanceJoint2D joint;

        private void Awake()
        {
            _tongue = FindObjectOfType<TongueController>();
            _player = _tongue.GetComponent<PlayerBehavior>();
            joint = GetComponent<DistanceJoint2D>();
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag("Ground"))
            {
                joint.enabled = true;
                _tongue.currentState = TongueController.State.Attached;
            }
            else if (other.CompareTag("GroundEnemy"))
            {
                joint.enabled = true;
                _tongue.currentState = TongueController.State.Attached;
            }
        }
    }
}