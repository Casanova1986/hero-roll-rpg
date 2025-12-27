using System;
using UnityEngine;

namespace HeroRoll.Battle
{
    public class HeroModelController : CharacterModelController
    {



        public void StartAttack(System.Action onComplete)
        {
            base.AttackAnim(() =>
            {
                onComplete?.Invoke();
            });
        }
    }
}
