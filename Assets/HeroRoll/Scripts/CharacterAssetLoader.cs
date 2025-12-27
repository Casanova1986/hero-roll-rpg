using System.Collections.Generic;
using NTHiep.Tool;
using Spine.Unity;
using UnityEngine;

namespace HeroRoll
{
    public class CharacterAssetLoader : MonoBehaviour
    {
        public GameObject _heroPrefab;
        public GameObject _monsterPrefab;
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
        public void SetSkeletonData(SkeletonAnimation skeletonAnimation, string idCharacter)
        {
            skeletonAnimation.skeletonDataAsset = _characterDataAssets.Get(idCharacter)._skeletonDataAsset;
            skeletonAnimation.ClearState();
            skeletonAnimation.Initialize(true);
        }

        #region SkeletonAnimation
        public void SetAnimationWait(SkeletonAnimation skeletonAnimation)
        {
            var state = skeletonAnimation.AnimationState;
            state.SetAnimation(0, "wait", true);
        }
        public void SetAnimationStart(SkeletonAnimation skeletonAnimation, bool isBoss = false, System.Action<float> complete = null)
        {
            var state = skeletonAnimation.AnimationState;
            if (isBoss)
            {
                state.SetAnimation(0, "ruchang", false);
                state.AddAnimation(0, "wait", true, 0);
            }
            else
            {
                state.SetAnimation(0, "wait", true);
            }
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