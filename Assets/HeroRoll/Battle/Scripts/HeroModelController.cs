using System;
using UnityEngine;

namespace HeroRoll.Battle
{
    public class HeroModelController : CharacterModelController
    {



        public void StartAttack(System.Action onComplete)
        {
            base.AttackAnim(true, () =>
            {
                onComplete?.Invoke();
            });
        }
        public void StartDeath(System.Action onComplete)
        {
            base.DeathAnim(() =>
            {
                onComplete?.Invoke();
            });
        }
    }
}
