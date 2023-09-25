using System;
using ScriptableObjects;
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
            Consuming,
            JustConsumed,
        }

        public LineRenderer line;
        public Transform hook;
        public float desiredAngle;

        private DistanceJoint2D _joint;
        private HookBehavior _hookBehavior;

        public float hookSpeed = 10f;
        public float maxLength;

        [HideInInspector] public PlayerBehavior player;

        private Vector2 _direction = Vector2.right + Vector2.down;
        public State currentState;

        private void Start()
        {
            line = hook.Find("Line").GetComponent<LineRenderer>();
            _hookBehavior = hook.gameObject.GetComponent<HookBehavior>();
            _joint = _hookBehavior.joint;
            player = GetComponent<PlayerBehavior>();
            line.positionCount = 2;
            line.endWidth = line.startWidth = 0.1f;
            line.SetPosition(0, transform.position);
            line.SetPosition(1, hook.position);
            line.useWorldSpace = true;
            currentState = State.Idle;
        }

        private void Update()
        {
            var transformPosition = transform.position;
            var hookPosition = hook.position;
            line.SetPosition(0, transformPosition);
            line.SetPosition(1, hookPosition);

            float distance = Vector2.Distance(transformPosition, hookPosition);
            Vector2 moveTowardSpeed = Vector2.MoveTowards(hookPosition, transformPosition, Time.deltaTime * hookSpeed);
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
                    if (distance > maxLength)
                    {
                        currentState = State.LineMax;
                    }

                    break;

                case State.LineMax:
                    hook.position = moveTowardSpeed;
                    if (distance < 0.1f)
                    {
                        BreakTheLine();
                    }

                    break;

                case State.Attached:
                    hook.SetParent(null);
                    if (_joint.distance > distance) _joint.distance = distance;
                    Vector2 dir = (Vector2)hook.position - (Vector2)transform.position;
                    float angle = Vector2.Angle(Vector2.right, dir);
                    if (Input.GetKeyUp(KeyCode.Space)) BreakTheLine();
                    if (Math.Abs(angle - desiredAngle) < 10F) BreakTheLine();
                    break;
                case State.Consuming:
                    hook.SetParent(null);
                    // if this object is too close to the player, destroy it else, pull it towards with the hook
                    if (distance < 0.1f)
                    {
                        // consume
                        currentState = State.JustConsumed;
                    }
                    else
                    {
                        // pull
                        hook.position = moveTowardSpeed;
                    }

                    break;
                case State.JustConsumed:
                    Score score = _hookBehavior.whatsOnTheHook;
                    Debug.Log(score.forceAmount);
                    player.rb.AddForce(score.forceAmount, ForceMode2D.Impulse);
                    _hookBehavior.whatsOnTheHook = null;
                    BreakTheLine();
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