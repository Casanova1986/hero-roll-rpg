using System;
using System.Collections.Generic;
using DG.Tweening;
using NTHiep.MiniOdin;
using Spine.Unity;
using UnityEngine;
using UnityEngine.Rendering;

namespace HeroRoll.Battle
{
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
        public virtual void SetInfoData(bool isHero)
        {
            if (isHero)
            {
                _infoCharacterBase = new InfoCharacterBase
                {
                    hp = PlayerController.instance._infoCharacterBase.hp,
                    atk = PlayerController.instance._infoCharacterBase.atk,
                    accuracy = PlayerController.instance._infoCharacterBase.accuracy,
                    critRate = PlayerController.instance._infoCharacterBase.critRate,
                    def = PlayerController.instance._infoCharacterBase.def,
                    eva = PlayerController.instance._infoCharacterBase.eva,
                    hpRegen = PlayerController.instance._infoCharacterBase.hpRegen,
                    trueDamage = PlayerController.instance._infoCharacterBase.trueDamage,
                    cooldown = PlayerController.instance._infoCharacterBase.cooldown,
                };
            }
            else
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
                    cooldown = characterDataAsset._cooldown,
                };
            }
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
        protected virtual void AttackAnim(bool isHero, System.Action<float> startAnim, System.Action onAttack, System.Action completeAttack)
        {
            int originSorttingLayer = this.GetComponent<SortingGroup>().sortingOrder;
            this.GetComponent<SortingGroup>().sortingOrder = 2;
            if (isHero)
            {
                ConfigAnimSkeleton.SetAnimationHeroAttack(_skeletonAnimation, startAnim: startAnim, onAttack: onAttack, complete: () =>
                {
                    this.GetComponent<SortingGroup>().sortingOrder = originSorttingLayer;
                    completeAttack?.Invoke();
                });
            }
            else
            {
                ConfigAnimSkeleton.SetAnimationMonsterAttack(_skeletonAnimation, startAnim: startAnim, onAttack: onAttack, complete: () =>
                {
                    this.GetComponent<SortingGroup>().sortingOrder = originSorttingLayer;
                    completeAttack?.Invoke();
                });
            }
        }
        public virtual void IdleAnim(bool isHero)
        {
            if (isHero)
            {
                ConfigAnimSkeleton.SetAnimationWait(_skeletonAnimation);
            }
            else
            {
                ConfigAnimSkeleton.SetAnimationWait(_skeletonAnimation);
            }
        }
        protected virtual void DeathAnim(bool isHero, System.Action completeDeath)
        {
            if (isHero)
            {
                ConfigAnimSkeleton.SetAnimationDeath(_skeletonAnimation, complete: () =>
                {
                    completeDeath?.Invoke();
                });
            }
            else
            {
                ConfigAnimSkeleton.SetAnimationDeath(_skeletonAnimation, complete: () =>
                {
                    completeDeath?.Invoke();
                });
            }
        }
        protected virtual void SkillAnim(bool isHero, List<System.Action> lsComplete)
        {
            if (isHero)
            {
                List<System.Action<Spine.Event>> lsActionComplete = new List<System.Action<Spine.Event>>();
                for (int i = 0; i < lsComplete.Count; i++)
                {
                    int index = i;
                    bool oneAction = false;
                    int countTrackAttack = 0;
                    System.Action<Spine.Event> actionComplete = (Event) =>
                    {
                        // Debug.Log("totalDuration: " + CakeDuration);
                        // Debug.Log("duration: " + duration);
                        if (index == 0 && Event.Data.Name == "chufa_1" && !oneAction)
                        {
                            oneAction = true;
                            // Debug.Log("=== Hero Skill Event: chufa_1 ===");
                            lsComplete[index]?.Invoke();
                        }
                        if (index > 0 && Event.Data.Name == "chufa" && !oneAction && countTrackAttack == 3)
                        {
                            oneAction = true;
                            // Debug.Log("=== Hero Skill Event: chufa ===");
                            lsComplete[index]?.Invoke();
                        }
                        if (index > 0 && Event.Data.Name == "chufa_2" && !oneAction)
                        {
                            oneAction = true;
                            // Debug.Log("=== Hero Skill Event: chufa_2 ===");
                            lsComplete[index]?.Invoke();
                        }
                        if (Event.Data.Name == "chufa")
                        {
                            countTrackAttack++;
                        }
                    };

                    lsActionComplete.Add(actionComplete);
                }


                ConfigAnimSkeleton.SetAnimationHeroSkill(_skeletonAnimation, 0, lsComplete: lsActionComplete);
            }
            else
            {
                List<System.Action<Spine.Event>> lsActionComplete = new List<System.Action<Spine.Event>>();
                for (int i = 0; i < lsComplete.Count; i++)
                {
                    int index = i;
                    bool oneAction = false;
                    int countTrackAttack = 0;

                    if (index == 0 && !oneAction)
                    {
                        oneAction = true;
                        // Debug.Log("=== Monster Skill Event: chufa_1 ===");
                        lsComplete[index]?.Invoke();
                    }
                    System.Action<Spine.Event> actionComplete = (Event) =>
                    {
                        if (index > 0 && Event.Data.Name == "chufa" && !oneAction && countTrackAttack == 2)
                        {
                            oneAction = true;
                            // Debug.Log("=== Monster Skill Event: chufa ===");
                            lsComplete[index]?.Invoke();
                        }
                        if (index > 0 && Event.Data.Name == "chuangjian" && !oneAction)
                        {
                            oneAction = true;
                            // Debug.Log("=== Monster Skill Event: chuangjian ===");
                            lsComplete[index]?.Invoke();
                        }
                        if (Event.Data.Name == "chufa")
                        {
                            countTrackAttack++;
                        }
                    };

                    lsActionComplete.Add(actionComplete);
                }


                ConfigAnimSkeleton.SetAnimationMonsterSkill(_skeletonAnimation, 0, lsComplete: lsActionComplete);
            }
        }

        #endregion
    }

}