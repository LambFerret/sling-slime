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
        public float desiredAngle = 45F;

        private DistanceJoint2D _joint;
        private HookBehavior _hookBehavior;

        public float hookSpeed = 10f;
        public float maxLength;
        public Transform whatsOnTheHook;

        [HideInInspector] public PlayerBehavior player;

        private Vector2 _direction = Vector2.right + Vector2.down;
        public State currentState;
        public float pullingSpeed = 30F;

        private void Awake()
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
                    if (_joint.distance > maxLength) currentState = State.LineMax;

                    player.rb.velocity = new Vector2(player.rb.velocity.x, 0);
                    Vector2 dir = (Vector2)transform.position - (Vector2)hook.position;
                    float angle = Vector2.Angle(Vector2.left, dir);
                    if (angle > desiredAngle) currentState = State.Consuming;
                    break;
                case State.Consuming:
                    hook.SetParent(null);
                    whatsOnTheHook.SetParent(hook);
                    // if this object is too close to the player, destroy it else, pull it towards with the hook
                    if (distance < 0.1f)
                    {
                        // consume
                        currentState = State.JustConsumed;
                    }
                    else
                    {
                        // pull
                        hook.position = Vector2.MoveTowards(hookPosition, transformPosition,
                            Time.deltaTime * pullingSpeed);
                    }

                    break;
                case State.JustConsumed:
                    player.rb.velocity = new Vector2(player.rb.velocity.x, 0);
                    player.rb.AddForce(whatsOnTheHook.GetComponent<ScoreBehavior>().score.forceAmount,
                        ForceMode2D.Impulse);
                    BreakTheLine();
                    break;
            }
        }

        private void BreakTheLine()
        {
            whatsOnTheHook = null;
            currentState = State.Idle;
            _joint.enabled = false;
            hook.gameObject.SetActive(false);
        }

        public void TargetAttached(Collider2D target)
        {
            currentState = State.Attached;
            whatsOnTheHook = target.transform;
            hook.SetParent(whatsOnTheHook);
        }
    }
}