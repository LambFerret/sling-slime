using System;
using DG.Tweening;
using ScriptableObjects;
using UnityEngine;

namespace sling
{
    public class PlayableSlime : MonoBehaviour
    {
        private bool _isDragging;
        private bool _thisIsLoaded;
        private SlingerBehavior _slinger;
        public Slime slime;
        public Rigidbody2D rb;
        private Camera _cam;
        private Vector3 offset;

        private GameObject _title;

        private void Awake()
        {
            _cam = Camera.main;
            rb = GetComponent<Rigidbody2D>();
        }

        private void Start()
        {
            _title = GameObject.Find("Title");
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

            if (_isDragging)
            {
                Vector3 newPosition =
                    _cam.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 10.0f));
                transform.position = newPosition + offset;
            }
        }

        private void OnMouseDown()
        {
            if (_title is not null)
            {
                _title.transform.DOMoveY(10f, 1f).SetEase(Ease.OutBounce)
                    .OnComplete(() => { _title.SetActive(false); });
            }

            _isDragging = true;
            offset = transform.position -
                     _cam.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 10.0f));
        }

        private void OnMouseUp()
        {
            _isDragging = false;
        }
    }
}