using System;
using UnityEngine;

namespace run
{
    public class HookBehavior : MonoBehaviour
    {

        private TongueController _tongue;
        public DistanceJoint2D joint;

        private void Awake()
        {
            _tongue = FindObjectOfType<TongueController>();
            joint = GetComponent<DistanceJoint2D>();
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag("Ground"))
            {
                joint.enabled = true;
                _tongue.currentState = TongueController.State.Attached;
            }
        }
    }
}