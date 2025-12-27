using System;
using DG.Tweening;
using NTHiep.MiniOdin;
using Spine.Unity;
using UnityEngine;
using UnityEngine.Rendering;

namespace HeroRoll.Battle
{
    [Serializable]
    public class InfoCharacterBase
    {
        public int atk;
        public int hp;
        public int def;
        public int accuracy;
        public int trueDamage;
        public int critRate;
        public int eva;
        public int hpRegen;
    }

    public class CharacterModelController : MonoBehaviour
    {
        [Header("Var")]
        [SerializeField] SkeletonAnimation _skeletonAnimation;

        [Header("Value")]
        public string _idCharacter;
        public CharacterType _characterType;
        public InfoCharacterBase _infoCharacterBase = new InfoCharacterBase
        {
            atk = 10,
            hp = 20,
            def = 5,
        };


        #region Setter
        public virtual void SetSkeletonData()
        {
            CharacterAssetLoader.instance.SetSkeletonData(_skeletonAnimation, _idCharacter);
        }
        public virtual void SetInfoData()
        {
            CharacterDataAsset characterDataAsset = CharacterAssetLoader.instance._characterDataAssets.Get(_idCharacter);
            _infoCharacterBase = new InfoCharacterBase
            {
                atk = characterDataAsset._atk,
                hp = characterDataAsset._hp,
                def = characterDataAsset._def,
                accuracy = characterDataAsset._accuracy,
                trueDamage = characterDataAsset._trueDamage,
                critRate = characterDataAsset._critRate,
                eva = characterDataAsset._eva,
                hpRegen = characterDataAsset._hpRegen,
            };
        }
        #endregion

        #region Getter
        public virtual int CalculateDame(int atk, int def)
        {
            return atk - def;
        }
        #endregion

        #region Animation
        protected virtual void AttackAnim(System.Action completeAttack)
        {
            int originSorttingLayer = this.GetComponent<SortingGroup>().sortingOrder;
            this.GetComponent<SortingGroup>().sortingOrder = 2;
            CharacterAssetLoader.instance.SetAnimationAttack(_skeletonAnimation, complete: (duration) =>
            {
                DOVirtual.DelayedCall(duration, () =>
                {
                    this.GetComponent<SortingGroup>().sortingOrder = originSorttingLayer;
                    completeAttack?.Invoke();
                });
            });
        }
        public virtual void IdleAnim()
        {
            CharacterAssetLoader.instance.SetAnimationWait(_skeletonAnimation);
        }

        #endregion
    }

}