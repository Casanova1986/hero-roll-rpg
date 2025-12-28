using UnityEngine;

namespace HeroRoll.Battle
{
    public class MonsterModelController : CharacterModelController
    {
        public void StartAttack(System.Action onComplete)
        {
            base.AttackAnim(false, () =>
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
