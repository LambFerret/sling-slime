using System;
using UnityEngine;

namespace sling
{
    public class Slime : MonoBehaviour
    {
        private bool _isDragging;
        private bool _thisIsLoaded;
        private SlingerBehavior _slinger;
        public Rigidbody2D rb;
        private Camera _cam;

        private void Awake()
        {
            _cam = Camera.main;
            rb = GetComponent<Rigidbody2D>();
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (!_thisIsLoaded && other.CompareTag("Slinger"))
            {
                _thisIsLoaded = true;
                _isDragging = false;
                other.GetComponent<SlingerBehavior>().Load(this);
            }
        }

        private void Update()
        {
            if (_thisIsLoaded) return;

            if (Input.GetMouseButtonDown(0))
            {
                Vector2 mousePos = _cam.ScreenToWorldPoint(Input.mousePosition);
                if (Vector2.Distance(mousePos, transform.position) < 1.0f)
                {
                    _isDragging = true;
                }
            }

            if (Input.GetMouseButtonUp(0)) _isDragging = false;
            if (_isDragging) DragSlime();
        }

        private void DragSlime()
        {
            Vector2 mousePos = _cam.ScreenToWorldPoint(Input.mousePosition);
            transform.position = mousePos;
        }
    }
}