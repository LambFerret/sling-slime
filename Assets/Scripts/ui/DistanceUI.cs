using System;
using UnityEngine;
using UnityEngine.UI;

namespace ui
{
    public class DistanceUI : MonoBehaviour
    {
        private Scrollbar _bar;

        private void Awake()
        {
            _bar = GetComponent<Scrollbar>();
        }

        public void UpdateValue(Component sender, object data)
        {
            if (data is not float f) return;
            f = f switch
            {
                < 0 => 0,
                > 1 => 1,
                _ => f
            };
            _bar.value = f;
        }
    }
}