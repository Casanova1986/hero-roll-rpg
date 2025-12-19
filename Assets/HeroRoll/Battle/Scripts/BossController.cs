using DG.Tweening;
using NTHiep.MiniOdin;
using Spine.Unity;
using UnityEngine;

namespace HeroRoll.Battle
{
    public class BossController : MonoBehaviour
    {
        [Header("Var")]
        [SerializeField] SkeletonAnimation _skeletonAnimationBoss;

        [Header("Value")]
        public int _timeTurnBossAttack;

        // public void 

        void Start()
        {
            SetAnimationStart();
        }




        #region Animation
        [Button]
        public void SetAnimationWait()
        {
            var state = _skeletonAnimationBoss.AnimationState;
            state.SetAnimation(0, "wait", true);
        }
        [Button]
        public void SetAnimationStart()
        {
            var state = _skeletonAnimationBoss.AnimationState;

            state.SetAnimation(0, "ruchang", false);
            state.AddAnimation(0, "wait", true, 0);
        }
        public void SetAnimationAttack(System.Action<float> complete)
        {
            var state = _skeletonAnimationBoss.AnimationState;
            state.SetAnimation(0, "atk1", false);
            state.AddAnimation(0, "wait", true, 0);
            complete?.Invoke(state.TimeScale);
        }
        #endregion
    }
}