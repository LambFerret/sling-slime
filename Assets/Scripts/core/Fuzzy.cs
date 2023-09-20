using UnityEngine;

namespace core
{
    internal static class Fuzzy
    {
        public static bool IsEqual(this float a, float b, float epsilon = 0.1f)
        {
            return Mathf.Abs(a - b) < epsilon;
        }

        public static bool IsEqual(this Vector3 a, Vector3 b, float epsilon = 0.1f)
        {
            return Mathf.Abs(a.x - b.x) < epsilon && Mathf.Abs(a.y - b.y) < epsilon && Mathf.Abs(a.z - b.z) < epsilon;
        }

        public static bool IsGreaterThan(this float a, float b, float epsilon = 0.1f)
        {
            if (epsilon == 0) return a > b;
            return a > b || Random.value > (a - b) / (epsilon * b);
        }

        public static bool IsLessThan(this float a, float b, float epsilon = 0.1f)
        {
            if (epsilon == 0) return a < b;
            return a < b || Random.value > (b - a) / (epsilon * b);
        }
    }
}