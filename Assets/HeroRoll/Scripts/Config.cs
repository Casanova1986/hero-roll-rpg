using System.Collections.Generic;
using Spine.Unity;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace HeroRoll
{
    public class Config
    {

    }
    public class ConfigScene
    {
        public const string Splash_Scene = "Splash";
        public const string Battle_Scene = "BattleScene";
        public const string Home_Scene = "HomeScene";

        public static void Change_SplashScene()
        {
            SceneManager.LoadScene(Splash_Scene);
        }
        public static void Change_HomeScene()
        {
            SceneManager.LoadScene(Home_Scene);
        }
        public static void Change_BattleScene()
        {
            SceneManager.LoadScene(Battle_Scene);
        }
    }
    public class ConfigBattle
    {
        public const int TurnBossAttack = 5;
    }
    public class ConfigAnimSkeleton
    {
        public const string NameAnim_Idle = "wait";
        public const string NameAnim_Ruchang = "ruchang";
        public static readonly string[] NameAnimHero_Attack = { "atk1", "atk2" };
        public static readonly string[] NameAnimMonter_Attack = { "atk1", "atk2" };
        public static readonly List<List<string>> NameAnimHero_Skill = new List<List<string>> { new List<string> { "skill1_1", "skill1_2", "skill1_3" } };
        public static readonly List<List<string>> NameAnimMonster_Skill = new List<List<string>> { new List<string> { "skill1_1", "skill1_2", "skill1_3" } };
        public const string NameAnim_Death = "death";
        public const string NameAnim_Stun = "stun";


        #region SkeletonAnimation
        //// Idle
        public static void SetAnimationWait(SkeletonAnimation skeletonAnimation)
        {
            var state = skeletonAnimation.AnimationState;
            state.SetAnimation(0, NameAnim_Idle, true);
        }
        //// Start
        public static void SetAnimationStart(SkeletonAnimation skeletonAnimation, bool isBoss = false, System.Action complete = null)
        {
            var state = skeletonAnimation.AnimationState;
            Spine.TrackEntry entry;
            if (isBoss)
            {
                entry = state.SetAnimation(0, NameAnim_Ruchang, false);
                state.AddAnimation(0, NameAnim_Idle, true, 0);

                entry.Complete += e =>
                {
                    complete?.Invoke();
                };
            }
            else
            {
                state.SetAnimation(0, NameAnim_Idle, true);
            }
        }
        //// Attack
        public static void SetAnimationHeroAttack(
            SkeletonAnimation skeletonAnimation,
            int index = 0,
            System.Action<float> startAnim = null,
            System.Action onAttack = null,
            System.Action complete = null)
        {
            var state = skeletonAnimation.AnimationState;
            Spine.TrackEntry entry = state.SetAnimation(0, NameAnimHero_Attack[index], false);
            state.AddAnimation(0, NameAnim_Idle, true, 0);


            startAnim?.Invoke(entry.Animation.Duration);

            entry.Complete += e =>
            {
                complete?.Invoke();
            };

            bool onceTime = false;
            entry.Event += (trackEntry, spineEvent) =>
            {
                if (!onceTime)
                {
                    onceTime = true;
                    // Debug.Log("<color=red>Event: " + spineEvent.Data.Name + " duration: " + trackEntry.AnimationTime);
                    onAttack?.Invoke();
                }
            };
        }
        public static void SetAnimationMonsterAttack(
            SkeletonAnimation skeletonAnimation,
            System.Action<float> startAnim = null,
            System.Action onAttack = null,
            System.Action complete = null)
        {
            var state = skeletonAnimation.AnimationState;
            var entry = state.SetAnimation(0, NameAnimMonter_Attack[0], false);
            state.AddAnimation(0, NameAnim_Idle, true, 0);

            startAnim?.Invoke(entry.Animation.Duration);
            entry.Complete += e =>
            {
                complete?.Invoke();
            };
            bool onceTime = false;
            entry.Event += (trackEntry, spineEvent) =>
            {
                if (!onceTime)
                {
                    onceTime = true;
                    // Debug.Log("<color=red>Event: " + spineEvent.Data.Name + " duration: " + trackEntry.AnimationTime);
                    onAttack?.Invoke();
                }
            };
        }
        //// Skill
        public static void SetAnimationHeroSkill(SkeletonAnimation skeletonAnimation, int index = 0, List<System.Action<Spine.Event>> lsComplete = null)
        {
            // Debug.Log($"<color=green>SetAnimationHeroSkill: {lsComplete?.Count}");
            if (index < 0 || index >= NameAnimHero_Skill.Count)
            {
                Debug.Log($"<color=red>Wrong index: {index}");
                return;
            }

            var state = skeletonAnimation.AnimationState;

            for (int i = 0; i < NameAnimHero_Skill[index].Count; i++)
            {
                int actionIndex = i;
                string animName = NameAnimHero_Skill[index][i];

                Spine.TrackEntry entry;

                if (i == 0)
                {
                    entry = state.SetAnimation(0, animName, false);
                }
                else
                {
                    entry = state.AddAnimation(0, animName, false, 0);
                }

                entry.Event += (trackEntry, spineEvent) =>
                {
                    // Debug.Log($"<color=red>Event= {spineEvent.Data.Name} | " + $"<color=white>Int= {spineEvent.Int} | " + $"<color=green>Float= {spineEvent.Float} | " + $"<color=blue>String= {spineEvent.String}");
                    if (spineEvent.Data.Name == "chufa_2")
                    {
                        state.AddAnimation(0, NameAnim_Idle, true, 0);
                    }
                    if (lsComplete != null && actionIndex < lsComplete.Count)
                    {
                        lsComplete[actionIndex]?.Invoke(spineEvent);
                    }
                };
            }
        }
        public static void SetAnimationMonsterSkill(SkeletonAnimation skeletonAnimation, int index = 0, List<System.Action<Spine.Event>> lsComplete = null)
        {
            // Debug.Log($"<color=green>SetAnimationMonsterSkill: {lsComplete?.Count}");
            if (index < 0 || index >= NameAnimMonster_Skill.Count)
            {
                Debug.Log($"<color=red>Wrong index: {index}");
                return;
            }

            var state = skeletonAnimation.AnimationState;

            for (int i = 0; i < NameAnimMonster_Skill[index].Count; i++)
            {
                int actionIndex = i;
                string animName = NameAnimMonster_Skill[index][i];

                Spine.TrackEntry entry;

                if (i == 0)
                {
                    entry = state.SetAnimation(0, animName, false);
                }
                else
                {
                    entry = state.AddAnimation(0, animName, false, 0);
                }

                entry.Event += (trackEntry, spineEvent) =>
                {
                    // Debug.Log($"<color=red>Index: {actionIndex} | Event= {spineEvent.Data.Name} | " + $"<color=white>Int= {spineEvent.Int} | " + $"<color=green>Float= {spineEvent.Float} | " + $"<color=blue>String= {spineEvent.String}");
                    if (spineEvent.Data.Name == "chuangjian")
                    {
                        state.AddAnimation(0, NameAnim_Idle, true, 0);
                    }
                    if (lsComplete != null && actionIndex < lsComplete.Count)
                    {
                        lsComplete[actionIndex]?.Invoke(spineEvent);
                    }
                };
            }
        }

        //// Death
        public static void SetAnimationDeath(SkeletonAnimation skeletonAnimation, System.Action complete = null)
        {
            var state = skeletonAnimation.AnimationState;
            var entry = state.SetAnimation(0, NameAnim_Death, false);
            entry.Complete += e =>
            {
                complete?.Invoke();
            };
        }
        //// Stun
        public static void SetAnimationStun(SkeletonAnimation skeletonAnimation, System.Action complete = null)
        {
            var state = skeletonAnimation.AnimationState;
            var entry = state.SetAnimation(0, NameAnim_Stun, false);

            entry.Complete += e =>
            {
                complete?.Invoke();
            };
        }
        #endregion
    }
}