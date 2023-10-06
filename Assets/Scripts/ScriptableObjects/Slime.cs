using System;
using UnityEngine;

namespace ScriptableObjects
{
    [CreateAssetMenu(fileName = "Slime", menuName = "ScriptableObject/Slime")]
    public class Slime : ScriptableObject
    {
        public enum SlimeType
        {
            Wind,
            Fire,
            Lightening
        }

        public string ID = Guid.NewGuid().ToString();
        public string slimeName;
        public SlimeType slimeType;
        public float power;
        public float speed;
        public float health;

        public Sprite sprite;

        public float MultiplyByType()
        {
            return slimeType switch
            {
                // 시간에 따라 감소하는 스피드 반감
                SlimeType.Wind => 0.5F,
                // 속도, 체력이 증가 효과를 받을 때 120%의 효과를 받음
                SlimeType.Fire => 1.2F,
                // 오브젝트와 부딪혔을 때 잃는 속도, 체력 반감
                SlimeType.Lightening => 0.5F,
                _ => 1F
            };
        }
    }
}