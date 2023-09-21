using System;
using events;
using ScriptableObjects;
using UnityEngine;

namespace run
{
    public class ScoreBehavior : MonoBehaviour
    {
        private Collider2D _col;

        public Score score;
        private GameObject _prefab;

        private void Awake()
        {
            _col = GetComponent<Collider2D>();
        }


        public int CalculateScore()
        {
            return score.value;
        }

        public bool IsFlying()
        {
            return score.scoreType == Score.ScoreType.Flying;
        }


    }
}