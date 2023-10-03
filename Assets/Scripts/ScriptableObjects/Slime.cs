using UnityEngine;

namespace ScriptableObjects
{
    [CreateAssetMenu(fileName = "Slime", menuName = "ScriptableObject/Slime")]
    public class Slime : ScriptableObject
    {
        public enum SlimeType
        {
            Type1,
            Type2,
            Type3
        }

        public SlimeType slimeType;
        public float power;
        public float speed;
        public float health;

        public Sprite sprite;
    }
}