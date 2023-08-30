using UnityEngine;

namespace ScriptableObjects
{
    [CreateAssetMenu(fileName = "Score", menuName = "ScriptableObject/Score")]
    public class Score : ScriptableObject
    {
        public enum ScoreType
        {
            Flying, Ground
        }

        public ScoreType scoreType;
        public string scoreName;
        public int value;
        public float sizeValue;
    }
}