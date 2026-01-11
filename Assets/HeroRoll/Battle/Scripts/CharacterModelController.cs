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
        [ColorHeader("<color=red>Var")]
        [SerializeField] SkeletonAnimation _skeletonAnimation;

        [ColorHeader("<color=red>Value")]
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
        public virtual int UpdateHeath(int value)
        {
            _infoCharacterBase.hp += value;
            if (_infoCharacterBase.hp < 0)
            {
                return -_infoCharacterBase.hp;
            }
            return 0;
        }


        #endregion

        #region Getter
        public virtual int CalculateDame(int atk, int def)
        {
            if (atk <= def)
            {
                return 0;
            }
            else
            {
                return atk - def;
            }
        }
        #endregion

        #region Animation
        protected virtual void AttackAnim(bool isHero, System.Action completeAttack)
        {
            int originSorttingLayer = this.GetComponent<SortingGroup>().sortingOrder;
            this.GetComponent<SortingGroup>().sortingOrder = 2;
            if (isHero)
            {
                CharacterAssetLoader.instance.SetAnimationHeroAttack(_skeletonAnimation, complete: (duration) =>
                {
                    DOVirtual.DelayedCall(duration, () =>
                    {
                        this.GetComponent<SortingGroup>().sortingOrder = originSorttingLayer;
                        completeAttack?.Invoke();
                    });
                });
            }
            else
            {
                CharacterAssetLoader.instance.SetAnimationMonsterAttack(_skeletonAnimation, complete: (duration) =>
                {
                    DOVirtual.DelayedCall(duration, () =>
                    {
                        this.GetComponent<SortingGroup>().sortingOrder = originSorttingLayer;
                        completeAttack?.Invoke();
                    });
                });
            }
        }
        public virtual void IdleAnim()
        {
            CharacterAssetLoader.instance.SetAnimationWait(_skeletonAnimation);
        }
        protected virtual void DeathAnim(System.Action completeDeath)
        {
            CharacterAssetLoader.instance.SetAnimationDeath(_skeletonAnimation, complete: (duration) =>
            {
                DOVirtual.DelayedCall(duration, () =>
                {
                    completeDeath?.Invoke();
                });
            });
        }

        #endregion
    }

}