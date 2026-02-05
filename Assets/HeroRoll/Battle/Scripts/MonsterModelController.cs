using System.Collections.Generic;
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
        public void StartSkill(System.Action onMove, System.Action onAttack, System.Action onComplete)
        {
            List<System.Action> onCompleteFunc = new List<System.Action>();

            switch (_idCharacter)
            {
                case "ShangZhang":
                    // onMove?.Invoke();
                    // onAttack?.Invoke();
                    // onComplete?.Invoke();
                    break;
            }

            onCompleteFunc.Add(onMove);
            onCompleteFunc.Add(onAttack);
            onCompleteFunc.Add(onComplete);

            base.SkillAnim(false, onCompleteFunc);
        }
    }
}
