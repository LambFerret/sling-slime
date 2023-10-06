using System.Collections;
using core;
using UnityEngine;
using DG.Tweening;
using etc;

namespace sling
{
    public class SlingerBehavior : MonoBehaviour
    {
        [Header("Slinger Rope")] public LineRenderer rope;
        public int maxDistance;
        public int minDistance;

        public float elasticity;

        private PlayableSlime _playableSlime;
        private bool _isDragging;
        private bool _isLoaded;
        private Camera _cam;

        private void Awake()
        {
            _cam = Camera.main;
            rope.enabled = false;
        }

        public void Load(PlayableSlime playableSlime)
        {
            _playableSlime = playableSlime;
            Transform slimeTransform = playableSlime.transform;
            slimeTransform.SetParent(transform);
            slimeTransform.transform.DOMove(transform.position, 1f).OnComplete(Reload);
        }

        public void Reload()
        {
            _playableSlime.transform.position = transform.position;
            _playableSlime.rb.velocity = Vector2.zero;
            _isLoaded = true;
            rope.enabled = false;
            _playableSlime.rb.isKinematic = true;
        }

        private void Update()
        {
            if (_playableSlime is null) return;

            if (Input.GetMouseButtonDown(0) && _isLoaded)
            {
                Vector2 mousePos = _cam.ScreenToWorldPoint(Input.mousePosition);
                if (Vector2.Distance(mousePos, transform.position) < 1.0f)
                {
                    _isDragging = true;
                    rope.enabled = true;
                    _playableSlime.rb.isKinematic = false;
                }
            }



            if (Input.GetMouseButtonUp(0) && _isDragging)
            {
                _isDragging = false;
                Vector2 directionToSlinger = (Vector2)transform.position - _playableSlime.rb.position;

                if (directionToSlinger.magnitude > minDistance)
                {
                    _playableSlime.rb.AddForce(directionToSlinger * elasticity, ForceMode2D.Impulse);
                    _playableSlime.transform.DOScale(0.1F, 0.5F);
                    _isLoaded = false;
                    GameDataManager.Instance.selectedSlime = _playableSlime.slime;
                    StartCoroutine(LoadScene());
                }
                else
                {
                    Reload();
                }

                rope.enabled = false;
            }

            if (_isDragging) DragSlime();

            rope.SetPosition(0, transform.position);
            rope.SetPosition(1, _playableSlime.transform.position);
        }


        private static IEnumerator LoadScene()
        {
            yield return new WaitForSeconds(0.2f);
            LoadingScreen.Instance.LoadScene("RunScene");
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

            _playableSlime.transform.position = transform.position + (Vector3)direction;
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