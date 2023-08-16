using System;
using UnityEngine;
using DG.Tweening;

namespace sling
{
    public class SlingerBehavior : MonoBehaviour
    {
        public LineRenderer rope;
        public int maxDistance;
        public int minDistance;
        public float elasticity;

        [SerializeField] private Slime _slime;
        [SerializeField] private bool _isDragging;
        [SerializeField] private bool _isLoaded;
        [SerializeField] private Camera _cam;

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

        private void DragSlime()
        {
            Vector2 mousePos = _cam.ScreenToWorldPoint(Input.mousePosition);
            Vector2 direction = mousePos - (Vector2)transform.position;

            if (direction.magnitude > maxDistance)
            {
                direction = direction.normalized * maxDistance;
            }

            _slime.transform.position = transform.position + (Vector3)direction;
        }
    }
}
