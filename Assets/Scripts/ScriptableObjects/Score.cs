using UnityEngine;

namespace ScriptableObjects
{
    [CreateAssetMenu(fileName = "Score", menuName = "ScriptableObject/Score")]
    public class Score : ScriptableObject
    {
        public string scoreName;
        public int value;
        public float sizeValue;
        public Sprite sprite;
    }
}