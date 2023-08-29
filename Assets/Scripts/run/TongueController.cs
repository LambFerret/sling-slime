using System;
using UnityEngine;

namespace run
{
    public class TongueController : MonoBehaviour
    {
        public enum State
        {
            Idle,
            Shooting,
            LineMax,
            Attached,
        }

        public LineRenderer line;
        public Transform hook;
        private DistanceJoint2D _joint;
        public float hookSpeed = 10f;
        public float maxLength;

        private Vector2 _direction = Vector2.right + Vector2.up;
        public State currentState;

        private void Start()
        {
            line = hook.Find("Line").GetComponent<LineRenderer>();
            _joint = hook.GetComponent<HookBehavior>().joint;
            line.positionCount = 2;
            line.endWidth = line.startWidth = 0.1f;
            line.SetPosition(0, transform.position);
            line.SetPosition(1, hook.position);
            line.useWorldSpace = true;
            currentState = State.Idle;
        }

        private void Update()
        {
            line.SetPosition(0, transform.position);
            line.SetPosition(1, hook.position);

            switch (currentState)
            {
                case State.Idle:
                    hook.SetParent(transform);
                    if (Input.GetKeyDown(KeyCode.Space))
                    {
                        hook.position = transform.position;
                        currentState = State.Shooting;
                        hook.gameObject.SetActive(true);
                    }

                    break;

                case State.Shooting:
                    hook.Translate(_direction.normalized * (Time.deltaTime * hookSpeed));
                    if (Vector2.Distance(transform.position, hook.position) > maxLength)
                    {
                        currentState = State.LineMax;
                    }

                    break;

                case State.LineMax:
                    hook.position = Vector2.MoveTowards(hook.position, transform.position, Time.deltaTime * hookSpeed);
                    if (Vector2.Distance(transform.position, hook.position) < 0.1f)
                    {
                        currentState = State.Idle;
                        hook.gameObject.SetActive(false);
                    }

                    break;

                case State.Attached:
                    hook.SetParent(null);
                    float currentDistance = Vector2.Distance(transform.position, hook.position);

                    // If joint distance is longer than current distance, set joint distance to current distance
                    if (_joint.distance > currentDistance)
                    {
                        _joint.distance = currentDistance;
                    }

                    if (Input.GetKeyUp(KeyCode.Space))
                    {
                        currentState = State.Idle;
                        _joint.enabled = false;
                        hook.gameObject.SetActive(false);
                        hook.SetParent(null);  // Detach the hook from the player
                    }
                    break;
            }
        }
    }
}