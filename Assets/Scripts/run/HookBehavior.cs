using ScriptableObjects;
using UnityEngine;

namespace run
{
    public class HookBehavior : MonoBehaviour
    {
        private TongueController _tongue;
        public DistanceJoint2D joint;

        public Score whatsOnTheHook;

        private void Awake()
        {
            _tongue = FindObjectOfType<TongueController>();
            joint = GetComponent<DistanceJoint2D>();
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag("LandObstacle"))
            {
                joint.enabled = true;
                _tongue.currentState = TongueController.State.Consuming;
                whatsOnTheHook = other.GetComponent<ScoreBehavior>().score;
                other.transform.SetParent(transform);
            }
            else if (other.CompareTag("Ground"))
            {
                // joint.enabled = true;
                // _tongue.currentState = TongueController.State.Attached;
            }
        }
    }
}