using core;
using ScriptableObjects;
using UnityEngine;

namespace run
{
    public class HookBehavior : MonoBehaviour
    {
        public DistanceJoint2D joint;
        public GameObject childToKeep;

        private TongueController _tongue;

        private void Awake()
        {
            _tongue = FindObjectOfType<TongueController>();
            joint = GetComponent<DistanceJoint2D>();
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag("LandObstacle"))
            {
                other.GetComponent<AIMoving>().Disable();
                joint.enabled = true;
                _tongue.TargetAttached(other);
            }
            else if (other.CompareTag("Ground"))
            {
                // joint.enabled = true;
                _tongue.currentState = TongueController.State.LineMax;
            }
        }

        public void ClearChildren()
        {
            var player = GameManager.instance.player;
            int count = 0;
            foreach (Transform child in transform)
            {
                if (child.gameObject != childToKeep)
                {
                    // if child doesn't have ScoreBehavior, then it's not a score. return
                    var score = child.GetComponent<ScoreBehavior>()?.score;
                    if (score is null) return;
                    Debug.Log("destroy" + child.gameObject.name);
                    player.rb.velocity = new Vector2(player.rb.velocity.x, 0);
                    player.rb.AddForce(score.forceAmount, ForceMode2D.Impulse);
                    child.gameObject.SetActive(false);
                    count++;
                }
            }

            Debug.Log(" count " + count + " children");

        }
    }
}