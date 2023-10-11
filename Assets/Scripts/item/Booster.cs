using System.Collections;
using core;
using UnityEngine;

namespace item
{
    public class Booster : Item
    {
        public float boostPower;

        public override void OnUse()
        {
            GameManager.instance.AddPlayerSpeed(boostPower);
            // y axis of player rb is fixed to that height
            GameManager.instance.player.rb.constraints |= RigidbodyConstraints2D.FreezePositionY;
        }

        public override void OnGet()
        {
            //
        }

        public override void OnEnd()
        {
            Debug.Log("duration over ");
            GameManager.instance.player.rb.constraints &= ~RigidbodyConstraints2D.FreezePositionY;
            GameManager.instance.AddPlayerSpeed(-boostPower);
        }
    }
}