using System;
using Cinemachine;
using UnityEngine;
using DG.Tweening;

namespace sling
{
    public class SlingerBehavior : MonoBehaviour
    {
        [Header("Slinger Rope")] public LineRenderer rope;
        public int maxDistance;
        public int minDistance;
        public float elasticity;
        [Header("Slinger Camera")] public CinemachineVirtualCamera virtualCamera;
        public float minZoom = 3f;
        public float maxZoom = 10f;
        public float zoomFactor = 1f; // 속도에 따른 줌 인/아웃 비율
        public float screenYSpeedAdjustment = 0.1f; // 이 값을 조절하여 ScreenY 변화 속도 조절 가능

        private Slime _slime;
        private bool _isDragging;
        private bool _isLoaded;
        private Camera _cam;

        private void Awake()
        {
            _cam = Camera.main;
            rope.enabled = false;
        }

        public void Load(Slime slime)
        {
            _slime = slime;
            Transform slimeTransform = slime.transform;
            slimeTransform.SetParent(transform);
            slimeTransform.transform.DOMove(transform.position, 1f).OnComplete(Reload);
        }

        public void Reload()
        {
            _slime.transform.position = transform.position;
            _slime.rb.velocity = Vector2.zero;
            _isLoaded = true;
            rope.enabled = false;
            _slime.rb.isKinematic = true;
        }

        private void Update()
        {
            if (_slime is null) return;

            if (Input.GetMouseButtonDown(0) && _isLoaded)
            {
                Vector2 mousePos = _cam.ScreenToWorldPoint(Input.mousePosition);
                if (Vector2.Distance(mousePos, transform.position) < 1.0f)
                {
                    _isDragging = true;
                    rope.enabled = true;
                    _slime.rb.isKinematic = false;
                }
            }

            if (IsThisOutOfView(_slime.transform))
            {
                ChangeCameraTarget(_slime.transform);
            }

            if (virtualCamera.LookAt is not null)
            {
                float verticalSpeed = _slime.rb.velocity.y;

                // 슬라임의 속도와 줌 팩터에 따라 줌을 조절
                float desiredZoom = virtualCamera.m_Lens.OrthographicSize + verticalSpeed * zoomFactor;
                // 슬라임의 속도에 따라 ScreenY 조절
                if (verticalSpeed > 0)
                {
                    virtualCamera.GetCinemachineComponent<CinemachineFramingTransposer>().m_ScreenY =
                        Mathf.MoveTowards(
                            virtualCamera.GetCinemachineComponent<CinemachineFramingTransposer>().m_ScreenY, 1,
                            screenYSpeedAdjustment);
                }
                else if (verticalSpeed < 0)
                {
                    virtualCamera.GetCinemachineComponent<CinemachineFramingTransposer>().m_ScreenY =
                        Mathf.MoveTowards(
                            virtualCamera.GetCinemachineComponent<CinemachineFramingTransposer>().m_ScreenY, -0F,
                            screenYSpeedAdjustment);
                }

                virtualCamera.m_Lens.OrthographicSize = Mathf.Clamp(desiredZoom, minZoom, maxZoom);
            }

            if (Input.GetMouseButtonUp(0) && _isDragging)
            {
                _isDragging = false;
                Vector2 directionToSlinger = (Vector2)transform.position - _slime.rb.position;

                if (directionToSlinger.magnitude > minDistance)
                {
                    _slime.rb.AddForce(directionToSlinger * elasticity, ForceMode2D.Impulse);
                    _isLoaded = false;
                }
                else
                {
                    Reload();
                }

                rope.enabled = false;
            }

            if (_isDragging) DragSlime();

            rope.SetPosition(0, transform.position);
            rope.SetPosition(1, _slime.transform.position);
        }

        private bool IsThisOutOfView(Transform t)
        {
            Vector3 screenPoint = _cam.WorldToViewportPoint(t.position);
            return screenPoint.x < 0 || screenPoint.x > 1 || screenPoint.y < 0 || screenPoint.y > 1;
        }

        private void DragSlime()
        {
            Vector2 mousePos = _cam.ScreenToWorldPoint(Input.mousePosition);
            Vector2 direction = mousePos - (Vector2)transform.position;

            float angle = Vector2.SignedAngle(Vector2.up, direction);

            if (angle is < 90 and > 0) // 270도보다 큰 경우
            {
                direction = RotateByAngle(Vector2.up, 90).normalized * direction.magnitude;
            }
            else if (angle is < 0 or 180) // 180도보다 작은 경우
            {
                direction = RotateByAngle(Vector2.up, 180).normalized * direction.magnitude;
            }

            if (direction.magnitude > maxDistance)
            {
                direction = direction.normalized * maxDistance;
            }

            _slime.transform.position = transform.position + (Vector3)direction;
        }

        private void ChangeCameraTarget(Transform newTarget)
        {
            virtualCamera.Follow = newTarget;
            virtualCamera.LookAt = newTarget;
        }

        private static Vector2 RotateByAngle(Vector2 vector, float angle)
        {
            return new Vector2(
                vector.x * Mathf.Cos(angle * Mathf.Deg2Rad) - vector.y * Mathf.Sin(angle * Mathf.Deg2Rad),
                vector.x * Mathf.Sin(angle * Mathf.Deg2Rad) + vector.y * Mathf.Cos(angle * Mathf.Deg2Rad)
            );
        }
    }
}