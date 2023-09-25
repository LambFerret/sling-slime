using UnityEngine;

namespace ScriptableObjects
{
    [CreateAssetMenu(fileName = "Score", menuName = "ScriptableObject/Score")]
    public class Score : ScriptableObject
    {
        public enum ScoreType
        {
            Air,
            Land
        }

        public ScoreType scoreType;
        public string scoreName;
        public int value;
        public float sizeValue;
        public Vector2 forceAmount;
    }
}