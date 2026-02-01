using UnityEngine;

namespace HeroRoll.Battle
{
    public class MonsterModelController : CharacterModelController
    {
        public void StartAttack(System.Action<float> startAnim, System.Action onAttack, System.Action onComplete)
        {
            base.AttackAnim(false, startAnim, onAttack, onComplete);
        }
        public void StartDeath(System.Action onComplete)
        {
            base.DeathAnim(false, () =>
            {
                onComplete?.Invoke();
            });
        }
    }
}
