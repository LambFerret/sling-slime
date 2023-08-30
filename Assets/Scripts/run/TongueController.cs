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
            Consuming
        }

        public LineRenderer line;
        public Transform hook;
        public float desiredAngle;

        private DistanceJoint2D _joint;
        public float hookSpeed = 10f;
        public float maxLength;

        private PlayerBehavior _player;

        private Vector2 _direction = Vector2.right + Vector2.up;
        public State currentState;

        private void Start()
        {
            line = hook.Find("Line").GetComponent<LineRenderer>();
            _joint = hook.GetComponent<HookBehavior>().joint;
            _player = GetComponent<PlayerBehavior>();
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
                    hook.gameObject.SetActive(false);
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

                    if (_joint.distance > currentDistance)
                    {
                        _joint.distance = currentDistance;
                    }

                    Vector2 dir = (Vector2)hook.position - (Vector2)transform.position;
                    float angle = Vector2.Angle(Vector2.right, dir);

                    if (Input.GetKeyUp(KeyCode.Space))
                    {
                        BreakTheLine();
                        hook.gameObject.SetActive(false);
                        _player.rb.velocity += new Vector2(5f, 0);

                    }

                    if (Math.Abs(angle - desiredAngle) < 10F)
                    {
                        BreakTheLine();
                        _player.rb.velocity -= new Vector2(5f, 0);
                    }

                    break;
                case State.Consuming:
                    // act consume animation
                    break;
            }
        }

        private void BreakTheLine()
        {
            currentState = State.Idle;
            _joint.enabled = false;
            hook.gameObject.SetActive(false);
        }
    }
}