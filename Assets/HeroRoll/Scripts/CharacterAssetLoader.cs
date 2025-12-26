using System.Collections.Generic;
using NTHiep.Tool;
using Spine.Unity;
using UnityEngine;

namespace HeroRoll
{
    public class CharacterAssetLoader : MonoBehaviour
    {
        public DictShow<string, CharacterDataAsset> _characterDataAssets;


        public static CharacterAssetLoader instance;

        /// <summary>
        /// Awake is called when the script instance is being loaded.
        /// </summary>
        void Awake()
        {
            if (CharacterAssetLoader.instance != null)
            {
                return;
            }
            instance = this;
        }

        #region SkeletonAnimation
        public void SetAnimationWait(SkeletonAnimation skeletonAnimation)
        {
            var state = skeletonAnimation.AnimationState;
            state.SetAnimation(0, "wait", true);
        }
        public void SetAnimationStart(SkeletonAnimation skeletonAnimation, System.Action<float> complete = null)
        {
            var state = skeletonAnimation.AnimationState;

            state.SetAnimation(0, "ruchang", false);
            state.AddAnimation(0, "wait", true, 0);
            complete?.Invoke(state.TimeScale);
        }
        public void SetAnimationAttack(SkeletonAnimation skeletonAnimation, int index = 1, System.Action<float> complete = null)
        {
            var state = skeletonAnimation.AnimationState;
            state.SetAnimation(0, $"atk{index}", false);
            state.AddAnimation(0, "wait", true, 0);
            complete?.Invoke(state.TimeScale);
        }

        #endregion
    }
}