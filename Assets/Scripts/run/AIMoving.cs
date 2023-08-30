using System;
using DG.Tweening;
using UnityEngine;

namespace run
{
    public class AIMoving :MonoBehaviour
    {
        public float movingDistance = 5f; // Distance the GameObject will move up and down
        public float duration = 2f;       // Time it takes to complete one up-and-down cycle
        public Ease easeType = Ease.InOutSine; // Type of easing to use

        public bool isVertical;
        private Vector3 _startingPosition;
        private Vector3 _endPosition;


        private void OnEnable()
        {
            _startingPosition = transform.position;
            _endPosition = isVertical ? new Vector3(transform.position.x, transform.position.y + movingDistance, transform.position.z) : new Vector3(transform.position.x + movingDistance, transform.position.y, transform.position.z);
            StartMoving();
        }

        private void StartMoving()
        {
            Sequence mySequence = DOTween.Sequence();
            mySequence.Append(transform.DOMove(_endPosition, duration).SetEase(easeType))
                .Append(transform.DOMove(_startingPosition, duration).SetEase(easeType))
                .SetLoops(-1, LoopType.Restart);
        }
    }
}