using System.Collections;
using core;
using UnityEngine;

namespace item
{
    public class Booster : Item
    {
        public float boostPower;

        public override void Use()
        {
            base.Use();
            GameManager.instance.AddPlayerSpeed(boostPower);
            StartCoroutine(Boost());
        }

        private IEnumerator Boost()
        {
            yield return new WaitForSeconds(duration);
            GameManager.instance.AddPlayerSpeed(-boostPower);
        }
    }
}