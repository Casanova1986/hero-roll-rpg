using System;
using System.Collections.Generic;
using UnityEngine;

namespace HeroRoll.Battle
{
    public class HeroModelController : CharacterModelController
    {

        public void StartAttack(Action<float> startAnim, System.Action onAttack, System.Action onComplete)
        {
            base.AttackAnim(true, startAnim, onAttack, onComplete);
        }
        public void StartDeath(System.Action onComplete)
        {
            base.DeathAnim(true, () =>
            {
                onComplete?.Invoke();
            });
        }
        public void StartSkill(System.Action onMove, System.Action onAttack, System.Action onComplete)
        {
            List<System.Action> onCompleteFunc = new List<System.Action>();

            switch (_idCharacter)
            {
                case "Xihe":
                    // onMove?.Invoke();
                    // onAttack?.Invoke();
                    // onComplete?.Invoke();
                    break;
            }

            onCompleteFunc.Add(onMove);
            onCompleteFunc.Add(onAttack);
            onCompleteFunc.Add(onComplete);

            base.SkillAnim(true, onCompleteFunc);
        }
        public void SetInfoCharacterBase(InfoCharacterBase infoCharacterBase)
        {
            this._infoCharacterBase = infoCharacterBase;
        }
    }

}
